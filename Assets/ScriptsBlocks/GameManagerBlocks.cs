using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyRoads3Dv3;

public enum Type {bus, tramvia, metrobus, turibus, bike, none};

public class GameManagerBlocks : MonoBehaviour {

	public static GameManagerBlocks instance = null;
	//Represents the points in the scene
	private UniducialLibrary.TuioManager m_TuioManager;

	//Represents the use of TUIO
	public bool Tuio = true;
	//Represents how many cubes are in each road
	public int numberPerType;
	//Represents how many cubes are in each road
	public float busheight = 0.1f;
	public float metrobusheight = 4.1f;
	public float turibusheight = 8.1f;
	public float tramviaheight = 12.1f;
	public float bikeheight = 16.1f;

	//Represents passenger buses
	public GameObject pointPrefab;
	//Represents passenger buses
	public GameObject busPrefab;
	//Represents Tramvia
	public GameObject tramviaPrefab;
	//Represents metroBus
	public GameObject metroBusPrefab;
	//Represents turiBus
	public GameObject turiBusPrefab;
	//Represents Bike
	public GameObject bikePrefab;
	//Represents bus dummy;
	private GameObject busDummy;
	//Represents metroBusDummy dummy;
	private GameObject metroBusDummy;
	//Represents turiBusDummy dummy;
	private GameObject turiBusDummy;
	//Represents tramviaDummy dummy;
	private GameObject tramviaDummy;
	//Represents bikeDummy dummy;
	private GameObject bikeDummy;
	//represents the roads in this order: bus, metrobus, turibus, tramvia, bike
	public GameObject[] roads;
	//represents roadObject
	public GameObject road;
	public int numberTrees = 50;
	//represents trees
	public GameObject tree1;
	public GameObject tree2;
	public GameObject tree3;
	public bool treeActive = false;
	public float width = 6.0f;
	public float diameter = 3.0f;

	//DEBUG
	public Type debugType;
	//Predefault points
	private Vector3[] testPoints;
	//Represents intersections
	public GameObject intersection;

	//counts to get next pointblock
	private int counter;
	private ArrayList existingIds;
	public ArrayList intersections;
	// Use this for initialization
	//SINGLETON AS A GAME MANAGER = JUST ONE INSTANCE OF THIS OBJECT.
	void Awake () 
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
		Setup ();
	}
	void Setup(){
		roads = new GameObject[5];
		roads[0] = Instantiate(road, Vector3.zero, Quaternion.identity);
		roads [0].GetComponent<Road> ().setType (Type.bus);
		roads[1] = Instantiate(road, Vector3.zero, Quaternion.identity);
		roads [1].GetComponent<Road> ().setType (Type.metrobus);
		roads[2] = Instantiate(road, Vector3.zero, Quaternion.identity);
		roads [2].GetComponent<Road> ().setType (Type.turibus);
		roads[3] = Instantiate(road, Vector3.zero, Quaternion.identity);
		roads [3].GetComponent<Road> ().setType (Type.tramvia);
		roads[4] = Instantiate(road, Vector3.zero, Quaternion.identity);
		roads [4].GetComponent<Road> ().setType (Type.bike);

		existingIds = new ArrayList ();
		//This creates a object based on prefab and put withing the scene
		//Instantiate(busPrefab, transform.position, Quaternion.identity);

		//testpoints created
		testPoints = new Vector3[21];
		testPoints[0] = new Vector3(100.0f, 0.0f , 0.0f);
		testPoints[1] = new Vector3(1.0f, 0.0f , 0.0f);
		testPoints[2] = new Vector3(0.0f, 0.0f , 50.0f);
		testPoints[3] = new Vector3(20.0f, 0.0f , 20.0f);
		testPoints[4] = new Vector3(-50.0f, 0.0f , -10.0f);
		testPoints[5] = new Vector3(50.0f, 0.0f , 50.0f);
		testPoints[6] = new Vector3(50.0f, 0.0f , -25.0f);
		testPoints[7] = new Vector3(10.0f, 0.0f , 65.0f);
		testPoints[8] = new Vector3(-20.0f, 0.0f , -35.0f);
		testPoints[9] = new Vector3(15.0f, 0.0f , 30.0f);
		testPoints[10] = new Vector3(10.0f, 0.0f , 50.0f);
		testPoints[11] = new Vector3(35.0f, 0.0f , -40.0f);
		testPoints[12] = new Vector3(-30.0f, 0.0f , 60.0f);
		testPoints[13] = new Vector3(20.0f, 0.0f , -15.0f);
		testPoints[14] = new Vector3(15.0f, 0.0f , 55.0f);
		testPoints[15] = new Vector3(0.0f, 0.0f , -15.0f);
		testPoints[16] = new Vector3(35.0f, 0.0f , 5.0f);
		testPoints[17] = new Vector3(-30.0f, 0.0f , 45.0f);
		testPoints[18] = new Vector3(-35.0f, 0.0f , -40.0f);
		testPoints[19] = new Vector3(-25.0f, 0.0f , 50.0f);
		testPoints[20] = new Vector3(20.0f, 0.0f , -5.0f);


		intersections = new ArrayList();
		this.m_TuioManager = UniducialLibrary.TuioManager.Instance;
		//createTrees ();
		//deactivateTrees ();
	}
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		keyInputs ();
		intersectionDetectionChecking ();
		//convexCollider ();

//		foreach (var item in intersections) {
//			Debug.Log (item);
//		}			
	}
	public bool pointExists(int id){
		//roads [0].GetComponent<Road>().;
		//return roads[0].GetComponent<Road>().pointExists(id);
		bool exists = false;
		foreach (int i in existingIds) {
			if (i == id) {
				exists = true;
			}
		}
		return exists;
	}
	public void setPositionPoint(int id, Vector3 position){
		//if that point is from that road
		Type t = idToType (id);
		int tp = typeToInt (t);
		roads [tp].GetComponent<Road>().setPosition(id, position);

	}
	//Adds new point
	public void addPoint(Vector3 position, int id){
		existingIds.Add (id);
		Type t = idToType (id);
		if (t !=  Type.none) {
			int tp = typeToInt (t);
			Vector3 positionHeight = idHeight (position, t);
			//roads [0].GetComponent<Road>().setType(tp);
			roads [tp].GetComponent<Road> ().addPoint (positionHeight, id);

			counter++;
		}else{
			//Debug.Log ("point is not vehicle");
		}
	}
	public ArrayList getPointsIdExists(){
		return existingIds;
	}
	public void deletePoint(int id){

		existingIds.Remove (id);
		Type t = idToType (id);
		int tp = typeToInt (t);
		roads [tp].GetComponent<Road>().deletePoint(id);
		//intersections = new ArrayList(); 
		if (counter > 0) {
			counter--;
		} else {
			counter = 0;
		}
	}
	void convexCollider(){
		GameObject r1 = GameObject.Find("roadbus");
		GameObject r2 = GameObject.Find("roadmetrobus");
		GameObject r3 = GameObject.Find("roadturibus");
		GameObject r4 = GameObject.Find("roadtramvia");
		GameObject r5 = GameObject.Find("roadbike");

		if (r1 != null) {
			r1.GetComponent<MeshCollider> ().convex = true;
		}
		if (r2 != null) {
			r2.GetComponent<MeshCollider> ().convex = true;
		}
		if (r3 != null) {
			r3.GetComponent<MeshCollider> ().convex = true;
		}
		if (r4 != null) {
			r4.GetComponent<MeshCollider> ().convex = true;
		}
		if (r5 != null) {
			r5.GetComponent<MeshCollider> ().convex = true;
		}
	}
	void keyInputs(){
		//key inputs dont work if TUIO is deleting guys
		//Everytime you press creates a point.
		if (Input.GetKeyDown("space"))
		{
			if (counter < testPoints.Length) {
				addPoint (testPoints [counter], counter);
				//Debug.Log ("Point added");
			}
		}
		if (Input.GetKeyDown(KeyCode.F)){
			debugType = Type.bus;
		}
		if (Input.GetKeyDown(KeyCode.G)){
			debugType = Type.metrobus;
		}
		if (Input.GetKeyDown(KeyCode.H)){
			debugType = Type.turibus;
		}
		if (Input.GetKeyDown(KeyCode.J)){
			debugType = Type.tramvia;
		}
		if (Input.GetKeyDown(KeyCode.K)){
			debugType = Type.bike;
		}
		for(int i = 0 ; i < 10; i++){
			if (Input.GetKeyDown (i+"")) {
				deletePoint (i);
			}
		}
	}
	public void deleteIntersection(string id){
		int i = int.Parse (id);
		//Debug.Log ("deleteing: " + i);
		intersections.Remove (i);
		GameObject.Find (id).transform.position = new Vector3 (1000,0,0);
		//Destroy (GameObject.Find (id));
	}
	public void deleteIntersection01(GameObject obj){
		//Destroy(obj);
		obj.transform.position = new Vector3(1000,0,0);
	}
	void addIntersection(Vector3 position, Type a, int sectionA, Type b, int sectionB, Vector3 a1, Vector3 a2, Vector3 b1, Vector3 b2, int i1, int i2, int i3, int i4){

		if (position != Vector3.zero) {

			string aa = typeToInt (a) + "";
			string secta = sectionA + "";
			string bb = typeToInt (b) + "";
			string sectb = sectionB + "";
			string idd1 = aa + secta + bb + sectb;
			string idd2 = bb + sectb + aa + secta;

			//Debug.Log ("this is the id: " + id);

			GameObject temp1 = GameObject.Find (idd1);
			GameObject temp2 = GameObject.Find (idd2);

			if (temp1 != null) {
				temp1.GetComponent<IntersectionBlocks> ().setPosition (position);
				temp1.GetComponent<IntersectionBlocks> ().setLinesPosition (a1, a2, b1, b2);
				temp1.GetComponent<IntersectionBlocks> ().setOrientation (a1);
				temp1.GetComponent<IntersectionBlocks> ().setIds (i1, i2, i3, i4);

			} else if (temp2 != null) {
				temp2.GetComponent<IntersectionBlocks> ().setPosition (position);
				temp2.GetComponent<IntersectionBlocks> ().setLinesPosition (a1, a2, b1, b2);
				temp2.GetComponent<IntersectionBlocks> ().setOrientation (a1);
				temp2.GetComponent<IntersectionBlocks> ().setIds (i1, i2, i3, i4);

			} else {
				GameObject inter = Instantiate (intersection, position, Quaternion.identity);
				inter.name = idd1;
				inter.GetComponent<IntersectionBlocks> ().setId (idd1);
				GameObject.Find (idd1).GetComponent<IntersectionBlocks> ().setOrientation (a1);
				GameObject.Find(idd1).GetComponent<IntersectionBlocks> ().setLinesPosition (a1,a2,b1,b2);
				GameObject.Find(idd1).GetComponent<IntersectionBlocks> ().setIds (i1, i2, i3, i4);

				//intersections.Add (id1);
			}
		}
	}
	public Vector3 intersectionDetection(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4){
		Vector3 intersection = Vector3.zero;

		var d = (p2.x - p1.x) * (p4.z - p3.z) - (p2.z - p1.z) * (p4.x - p3.x);

		if (d == 0.0f)
		{
			return intersection;
		}

		var u = ((p3.x - p1.x) * (p4.z - p3.z) - (p3.z - p1.z) * (p4.x - p3.x)) / d;
		var v = ((p3.x - p1.x) * (p2.z - p1.z) - (p3.z - p1.z) * (p2.x - p1.x)) / d;

		if (u < 0.0f || u > 1.0f || v < 0.0f || v > 1.0f)
		{
			return intersection;
		}

		intersection.x = p1.x + u * (p2.x - p1.x);
		intersection.z = p1.z + u * (p2.z - p1.z);

		return intersection;
	}
	void intersectionDetectionChecking(){
		Vector3 intersection = Vector3.zero;

		for (int i = 0; i < roads.Length; i++) {
			LinkedList<GameObject> camino = roads [i].GetComponent<Road> ().points;
			if (camino != null) {
				LinkedListNode<GameObject> caminoFirst = camino.First;
				if (caminoFirst != null) {
					int segment = 0;
					while ((caminoFirst.Next != null)) {
						for (int j = 0; j < roads.Length; j++) {
							if (i != j) {
								LinkedList<GameObject> camino02 = roads [j].GetComponent<Road> ().points;
								LinkedListNode<GameObject> camino02First = camino02.First;
								if (camino02First != null) {
									int segment02 = 0;
									while ((camino02First.Next != null)) {
										intersection = intersectionDetection (caminoFirst.Value.transform.position, caminoFirst.Next.Value.transform.position, camino02First.Value.transform.position, camino02First.Next.Value.transform.position);
										if (intersection != Vector3.zero) {
											addIntersection (intersection, roads [i].GetComponent<Road> ().type, segment, roads [j].GetComponent<Road> ().type, segment02, caminoFirst.Value.transform.position, caminoFirst.Next.Value.transform.position, camino02First.Value.transform.position, camino02First.Next.Value.transform.position, caminoFirst.Value.GetComponent<PointBlocks>().getId(), caminoFirst.Next.Value.GetComponent<PointBlocks>().getId(), camino02First.Value.GetComponent<PointBlocks>().getId(), camino02First.Next.Value.GetComponent<PointBlocks>().getId() );

											//intersectionsObjects [i].transform.position = intersection;
											//intersections.Add (intersectionsObjects [i]);
											//Debug.Log ("intersecta");
										} else {
											//intersectionsObjects [i].transform.position = new Vector3(1000,0,0);

										}
										segment02++;
										camino02First = camino02First.Next;
									}
								}
							}
						}
						segment++;
						caminoFirst = caminoFirst.Next;
					}
				}
			}
		}


	}

	public bool markerExists(int id){
		return m_TuioManager.IsMarkerAlive (id);
	}
	public int typeToInt(Type tp){
		int i = 0;
		if (tp == Type.bus) {
			i = 0;	
		}
		if (tp == Type.metrobus) {
			i = 1;	
		}		if (tp == Type.turibus) {
			i = 2;	
		}		if (tp == Type.tramvia) {
			i = 3;	
		}		if (tp == Type.bike) {
			i = 4;	
		}
		return i;
	}
	public Type idToType(int id){
		Type vehicle = Type.none;
		if (id >= 0 && id < numberPerType) {
			vehicle = Type.bus;
		}
		if (id >= numberPerType && id < numberPerType*2) {
			vehicle = Type.metrobus;
		}
		if (id >= numberPerType*2 && id < numberPerType*3) {
			vehicle = Type.turibus;
		}
		if (id >= numberPerType*3 && id < numberPerType*4) {
			vehicle = Type.tramvia;
		}
		if (id >= numberPerType*4 && id < numberPerType*5) {
			vehicle = Type.bike;
		}
		return vehicle;
	}
	public void createTrees(){
		for (int i = 0; i < numberTrees; i++) {
			Instantiate (tree1, new Vector3(0,0,0), Quaternion.identity);
			Instantiate (tree2, new Vector3(0,0,0), Quaternion.identity);
			Instantiate (tree3, new Vector3(0,0,0), Quaternion.identity);

		}
	}
	public void activateTrees(){
		GameObject[] trees = GameObject.FindGameObjectsWithTag ("Tree");
		for (int i = 0; i < trees.Length; i++) {
			trees[i].SetActive(true);
		}
		treeActive = true;

	}
	public void deactivateTrees(){
		GameObject[] trees = GameObject.FindGameObjectsWithTag ("Tree");
		for (int i = 0; i < trees.Length; i++) {
			trees[i].SetActive(false);
		}
		treeActive = false;
	}
	public Vector3 idHeight(Vector3 pos, Type type){
		Vector3 position = pos;
		Type t = type;
		float height = 0;
		if(t == Type.bus){
			height = GameManagerBlocks.instance.busheight;
		}
		else if(t == Type.metrobus){
			height = GameManagerBlocks.instance.metrobusheight;
		}			
		else if(t == Type.turibus){
			height = GameManagerBlocks.instance.turibusheight;
		}			
		else if(t == Type.tramvia){
			height = GameManagerBlocks.instance.tramviaheight;
		}			
		else if(t == Type.bike){
			height = GameManagerBlocks.instance.bikeheight;
		}
		position = new Vector3 (position.x, height, position.z);
		return position;
	}
	public void debugBus(){
		debugType = Type.bus;
	}
	public void debugMetroBus(){
		debugType = Type.metrobus;
	}
	public void debugTuriBus(){
		debugType = Type.turibus;
	}
	public void debugTram(){
		debugType = Type.tramvia;
	}
	public void debugBike(){
		debugType = Type.bike;
	}
}
