using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TUIO;


public class TUIOGM : MonoBehaviour {

	//PARAMETER ADDED BY BUSBLOCKS
	public float minX = -120.0f;
	public float maxX = 120.0f;
	public float minY = -67.5f;
	public float maxY = 67.5f;
	private float minTableX = 0.0f;
	private float maxTableX = 1.0f;
	private float minTableY = 0.0f;
	private float maxTableY = 1.0f;

	public Vector3 p1 = Vector3.zero;
	public Vector3 p2 = Vector3.zero;
	public Vector3 p3 = Vector3.zero;
	public Vector3 p4 = Vector3.zero;
	private Vector3 c = Vector3.zero;

	//relation 16/9 ratio
	public Vector3[] calibrationPoints;
	private int calibrarionFirstFiducial = 17;

	public GameObject prefab = null;

	//translation
	public bool InvertX = false;
	public bool InvertY = false;

	private UniducialLibrary.TuioManager m_TuioManager;



	void Awake()
	{
		this.m_TuioManager = UniducialLibrary.TuioManager.Instance;

		//uncomment next line to set port explicitly (default is 3333)
		//tuioManager.TuioPort = 7777;

		this.m_TuioManager.Connect();
	}
	void Start()
	{
		calibrationPoints = new Vector3[4];
	}
	void Update()
	{
		if (this.m_TuioManager.IsConnected) {
			List<TuioObject> markers = this.m_TuioManager.getMarkers();
			foreach (TuioObject tuioObject in markers.ToArray())
			{
				float x = tuioObject.getX();
				float y = tuioObject.getY();
				float mapX;
				float mapY;
				if (InvertX) {
					//space it's still 16:9
					mapX = mapValue(x, minTableX, maxTableX, minX, maxX);
					mapX = Mathf.Clamp (mapX, minX, maxX);
				} else {
					mapX = mapValue(x, minTableX, maxTableX, maxX, minX);
					mapX = Mathf.Clamp (mapX, minX, maxX);
				}
				if (InvertY) {
					mapY = mapValue(y, minTableY, maxTableY, maxY, minY);
					mapY = Mathf.Clamp (mapY, minY, maxY);

				} else {
					mapY = mapValue(y, minTableY, maxTableY, minY, maxY);
					mapY = Mathf.Clamp (mapY, minY, maxY);

				}
				Vector3 positionTemp = new Vector3 (mapX, 0.0f, mapY);
				//Debug.Log (tuioObject.getSymbolID ());
				bool exists = GameManagerBlocks.instance.pointExists(tuioObject.getSymbolID ());
				int idd = tuioObject.getSymbolID ();
				//Debug.Log (exists);
				if (exists) {
					GameManagerBlocks.instance.setPositionPoint(idd, positionTemp);
				} else {
					GameManagerBlocks.instance.addPoint(positionTemp, idd );
					//Debug.Log ("Added new point");
				}
				if (Input.GetKeyDown(KeyCode.C)) {
					setCalibrationPoints(idd);
					saveCalibrationPoints ();
				}
				if (Input.GetKeyDown(KeyCode.L)){
					loadCalibrationPoints ();
				}	
				if (Input.GetKeyDown(KeyCode.R)){
					resetCalibration ();
				}	
						//Debug.Log ("Calibration point: " + i + " added");
				//if(calibrationPoints[0] != Vector3.zero && calibrationPoints[1] != Vector3.zero && (idd == calibrarionFirstFiducial||idd == calibrarionFirstFiducial+1)){
					//Debug.Log ("Using calibration points");
					//GameManagerBlocks.instance.calibration (calibrationPoints);
					//Debug.Log ("Calibration points: minx: " + minTableX + ", maxx: " + maxTableX + ", miny: "+ minTableY + ", maxy: " + maxTableY);

				//}

			}

			///***DELETING***
//			ArrayList existingPoints = GameManagerBlocks.instance.getPointsIdExists ();
//			ArrayList existsCopy = new ArrayList ();
//			foreach (int item in existingPoints) {
//				existsCopy.Add (item);
//			}
//			//Debug.Log (existingPoints.ToString());
//			//Debug.Log (existingPoints.Count);
//
//			foreach (int id in existsCopy) {
//				//Debug.Log (id);
//				bool present = false;
//				TuioObject temp = null;
//				foreach (TuioObject tuioObject in markers) {
//					if (tuioObject.getSymbolID() == id) {
//						temp = tuioObject;
//						present = true;
//					}
//				}
//				if (!present) {
//					//Debug.Log(id + " should not be here");
//					//GameManagerBlocks.instance.deletePoint (id);
//				}					
//			}
			///***DELETING***

		}

	}
	public void setCalibrationPoints(int idd){
		if (idd == calibrarionFirstFiducial) {
			minTableX = m_TuioManager.GetMarker(calibrarionFirstFiducial).getX();
			minTableY = m_TuioManager.GetMarker(calibrarionFirstFiducial).getY();
			//Debug.Log ("position calibration 1: "  + positionTemp);
		}
		if (idd == calibrarionFirstFiducial+1) {
			maxTableY = m_TuioManager.GetMarker(calibrarionFirstFiducial+1).getY();
			maxTableX = m_TuioManager.GetMarker(calibrarionFirstFiducial+1).getX();

		}
	}
	public void setCalibrationPoints01(int idd){
		if (idd == calibrarionFirstFiducial) {
			p1.x = m_TuioManager.GetMarker(calibrarionFirstFiducial).getX();
			p1.y = m_TuioManager.GetMarker(calibrarionFirstFiducial).getY();
			//Debug.Log ("position calibration 1: "  + positionTemp);
		}
		if (idd == calibrarionFirstFiducial+1) {
			p2.x = m_TuioManager.GetMarker(calibrarionFirstFiducial+1).getY();
			p2.y = m_TuioManager.GetMarker(calibrarionFirstFiducial+1).getX();

		}		
		if (idd == calibrarionFirstFiducial+2) {
			p3.x = m_TuioManager.GetMarker(calibrarionFirstFiducial+2).getY();
			p3.y = m_TuioManager.GetMarker(calibrarionFirstFiducial+2).getX();

		}		
		if (idd == calibrarionFirstFiducial+3) {
			p4.x = m_TuioManager.GetMarker(calibrarionFirstFiducial+3).getY();
			p4.y = m_TuioManager.GetMarker(calibrarionFirstFiducial+3).getX();

		}
	}
	public void calculateC(){
		c = (p1 + p2 + p3 + p4) / 4;
	}
	public Vector3 calculateNormals(Vector3 a, Vector3 b){
		return (b - a).normalized;
	}
	public void transformation(Vector3 a){
		Vector3 r = Vector3.zero;
		//float x = 240 * Vector3.Dot (c - a);
		//float y = 240 * Vector3.Dot (c - a);
		//r.x = x;
		//r.y = y;

	}
	public Vector3[] getCalibrationPoints(){
		return calibrationPoints;
	}
	public float mapValue(float value, float low1, float high1, float low2, float high2){
		float end =  low2 + (value - low1) * (high2 - low2) / (high1 - low1);
		return end;
	}
	public void saveCalibrationPoints(){
		PlayerPrefs.SetFloat ("minX", minTableX);
		PlayerPrefs.SetFloat ("minY", minTableY);		
		PlayerPrefs.SetFloat ("maxX", maxTableX);
		PlayerPrefs.SetFloat ("maxY", maxTableY);

	}
	public void loadCalibrationPoints(){
		minTableX = PlayerPrefs.GetFloat ("minX");
		minTableY = PlayerPrefs.GetFloat ("minY");
		maxTableX = PlayerPrefs.GetFloat ("maxX");
		maxTableY = PlayerPrefs.GetFloat ("maxY");
		Debug.Log (minTableX + " " + minTableY + " " + maxTableX + " " + maxTableX);

	}
	public void resetCalibration(){
		 minTableX = 0.0f;
		 maxTableX = 1.0f;
		 minTableY = 0.0f;
		 maxTableY = 1.0f;
		saveCalibrationPoints ();
		}
	void OnApplicationQuit()
	{
		if (this.m_TuioManager.IsConnected)
		{
			this.m_TuioManager.Disconnect();
		}
	}


}
