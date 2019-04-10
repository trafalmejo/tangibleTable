using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyRoads3Dv3;


#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used

public class Road : MonoBehaviour {

	public Type type;
	public ERRoadNetwork roadNet;
	public ERRoadType roadType;
	public ERRoad road;
	//RUNTIME ROAD
	public float resolution = 1000.0f;
	public float width = 6.0f;
	public LinkedList<GameObject> points;
	public Vector3[] pointsPosition;
	private GameObject prefabVehicle;
	private GameObject prefabPoint;
	private GameObject vehicle;

	void Setup(){

	}
	// Use this for initialization
	void Start () {
		// Create Road Network object
		width = GameManagerBlocks.instance.width;
		roadNet = new ERRoadNetwork();
		// create a new road type
		roadType = new ERRoadType();
		//width of the road
		roadType.roadWidth = width;
		// optional
		// roadType1.layer = 1;
		// roadType1.tag = "Untagged";
		// roadType.hasMeshCollider = false; // default is true
		// roadType = roadNetwork.GetRoadTypeByName("Train Rail");
		// Debug.Log(roadType.roadMaterial);
		//Materials/sidewalks/sidewalk
		//Materials/roads/road material
		if (type == Type.bus) {
			roadType.roadMaterial = Resources.Load ("Textures/Materials/01bus") as Material;
		}
		else if(type == Type.metrobus){
			roadType.roadMaterial = Resources.Load ("Textures/Materials/02metroBus") as Material;
		}
		else if(type == Type.turibus){
			roadType.roadMaterial = Resources.Load ("Textures/Materials/03turiBus") as Material;

		}		else if(type == Type.tramvia){
			roadType.roadMaterial = Resources.Load ("Textures/Materials/04tramvia") as Material;

		}		else if(type == Type.bike){
			roadType.roadMaterial = Resources.Load ("Textures/Materials/05bike") as Material;

		}

		road = roadNet.CreateRoad("road"+type, roadType);
		road.SetResolution(resolution);
		road.SetRoadType (roadType);
		road.SetMeshCollider (true);


		//road1.SetAngleThreshold (0.0f);
		//road1.SetSplatmap (true);
		//road1.SetWidth (30.0f);
		//connection = new ERConnection ();
		//road1.InsertIConnector (0);
		//road1 = roadNetwork1.CreateRoad("road 1", roadType1, pointsPosition);
		//road2 = roadNetwork1.CreateRoad("road2", roadType1, testPoints2);
		//list points in the road
		pointsPosition = new Vector3[20];
		points = new LinkedList<GameObject> ();
		//initiastes network
		EasyRoads3Dv3.ERRoadNetwork roadNetwork = new EasyRoads3Dv3.ERRoadNetwork();
		// initiates prefab
		prefabVehicle = GameManagerBlocks.instance.pointPrefab;
		if (type == Type.bus) {
			prefabVehicle = GameManagerBlocks.instance.busPrefab;
		}
		else if(type == Type.turibus)
		{
			prefabVehicle = GameManagerBlocks.instance.turiBusPrefab;
		}
		else if(type == Type.metrobus){
			prefabVehicle = GameManagerBlocks.instance.metroBusPrefab;
		}
		else if(type == Type.tramvia){
			prefabVehicle = GameManagerBlocks.instance.tramviaPrefab;
		}
		else if(type == Type.bike){
			prefabVehicle = GameManagerBlocks.instance.bikePrefab;
		}
		prefabPoint = GameManagerBlocks.instance.pointPrefab;
	}
	
	// Update is called once per frame
	void Update () {
		//si numero de nodos es mayor a 0
		if (points.Count > 0) {
			int i = 0;
			foreach (GameObject punto in points) {
				//cada posicion de nodo lo agrego a un vector de posiciones
				//EasyRoads3Dv3 roads maneja un arreglo []
				//Yo manejo un LinkedList de objetos
				pointsPosition [i] = punto.transform.position;
				i++;
			}
			for (int j = 0; j < pointsPosition.Length; j++) {
				if (pointsPosition [j] != null) {
					//copia la posiciones del vector al camino
						road.SetMarkerPosition (j, pointsPosition [j]);
						road.SetSplineStrength (j, 0.0f);

				}
			}
		}


	}

	void OnDestroy(){

		// Restore road networks that are in Build Mode
		// This is very important otherwise the shape of roads will still be visible inside the terrain!

		if(roadNet != null){
			if(roadNet.isInBuildMode){
				roadNet.RestoreRoadNetwork();
				Debug.Log("Restore Road Network");
			}
		}
	}
	public void setPosition( int id, Vector3 newPosition){
		foreach (GameObject punto in points) {
			if (punto.GetComponent<PointBlocks> ().getId () == id) {
				punto.GetComponent<PointBlocks> ().setPosition (newPosition);
			}
		}
	}
	public void setType(Type t){
		type = t;
	}
	public void addPoint(Vector3 position, int id){
		//Debug.Log (position);
		GameObject temp = Instantiate(prefabPoint, position, Quaternion.identity);
		temp.GetComponent<PointBlocks>().setType (type);
		temp.GetComponent<PointBlocks>().setId(id);
		//Debug.Log ("id added:" + temp.GetComponent<PointBlocks> ().getId ());
		float minDist = 900.0f;
		LinkedListNode<GameObject> nearest = null;
		LinkedListNode<GameObject> currentNode = points.First;
		while ((currentNode != null)) {
			float dist = Vector3.Distance(currentNode.Value.transform.position, temp.transform.position);
			if (dist < minDist)
			{
				nearest = currentNode;
				minDist = dist;
			}
			currentNode = currentNode.Next;
		}
		if (points.Count == 0) {
			points.AddLast (temp);
			road.InsertMarker (temp.transform.position);
		} else {
			if (nearest != null && temp != null) {
				points.AddBefore (nearest, temp);
				road.InsertMarker (temp.transform.position);
			} else {
				Debug.Log ("Didnt add anything");
			}
		}
		//points.AddLast (temp);
		resetVehicle();
	}
	public void deletePoint(int id){
		GameObject temp = null;
		int i = 0;
		int j = 0;

		foreach(GameObject punto in points){
			if (punto.GetComponent<PointBlocks>().getId() == id) {
				temp = punto;
				j = i;
			}
			i++;
		}
		if (temp != null) {
			road.DeleteMarker (j);
			points.Remove (temp);
			Destroy (temp);
			resetVehicle ();
		} else {
			Debug.Log ("is null");
		}

	}
	public void resetVehicle(){
		if (vehicle == null) {
			vehicle = Instantiate (prefabVehicle, road.GetMarkerPosition (0), Quaternion.identity);
			if (type == Type.tramvia) {
				vehicle.GetComponentInChildren<Vehicle> ().resetPosition ();
			} else {
				vehicle.GetComponent<Vehicle> ().resetPosition ();
			}
		} else {
			if (points.Count <= 0) {
				Destroy (vehicle);
			}
		} 
		//set waypoints
		if (vehicle != null) {
			if (type == Type.tramvia) {
				vehicle.GetComponentInChildren<Vehicle> ().resetPosition ();
				vehicle.GetComponentInChildren<Vehicle> ().transform.position = road.GetMarkerPosition (0);
				vehicle.GetComponentInChildren<Vehicle> ().setWaypoints (points);
			} else {
				vehicle.GetComponent<Vehicle> ().resetPosition ();
				vehicle.transform.position = road.GetMarkerPosition (0);
				vehicle.GetComponent<Vehicle> ().setWaypoints (points);
			}
		}
	}

}
