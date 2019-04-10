using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntersectionBlocks : MonoBehaviour {

	private string id;
	private Vector3 a1;
	private Vector3 a2;
	private Vector3 b1;
	private Vector3 b2;
	private int i1;
	private int i2;
	private int i3;
	private int i4;

	// Use this for initialization
	void Start () {
		gameObject.tag = "Intersection";
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (id);
		string i = id;
		//tipo seccion tipo seccion
		char[] ide = i.ToCharArray();
		int tipoA = int.Parse(ide[0]+"");
		int seccionA = int.Parse (ide[1]+"");
		int tipoB = int.Parse (ide[2]+"");
		int seccionB = int.Parse (ide[3]+"");
		Vector3 r1 = Vector3.zero;
		Vector3 r2 = Vector3.zero;

			Vector3 a = GameManagerBlocks.instance.roads [tipoA].GetComponent<Road> ().pointsPosition [seccionA];
			Vector3 b = GameManagerBlocks.instance.roads [tipoA].GetComponent<Road> ().pointsPosition [seccionA + 1];
			Vector3 aa = GameManagerBlocks.instance.roads [tipoB].GetComponent<Road> ().pointsPosition [seccionB];
			Vector3 bb = GameManagerBlocks.instance.roads [tipoB].GetComponent<Road> ().pointsPosition [seccionB + 1];


			r1 =	GameManagerBlocks.instance.intersectionDetection (a, b, aa, bb);
			r2 =	GameManagerBlocks.instance.intersectionDetection (aa, bb, a, b);
		//Debug.Log (a + " " + b +" " + aa + " "+ bb);
		//Debug.Log(r1 + " " + r2);
		//}
		//Debug.Log (r);
		if (r1 == Vector3.zero) {
			GameManagerBlocks.instance.deleteIntersection (id);
		} 
		if (r2 == Vector3.zero) {
			GameManagerBlocks.instance.deleteIntersection (id);
		}
		if(!GameManagerBlocks.instance.markerExists(i1) || !GameManagerBlocks.instance.markerExists(i2) || !GameManagerBlocks.instance.markerExists(i3) || !GameManagerBlocks.instance.markerExists(i4)){
			GameManagerBlocks.instance.deleteIntersection (id);
		}

	}
	public void setId(string idd){
		id = idd;
	}
	public void setPosition(Vector3 pos){
		Vector3 position = pos;
		position.y = a1.y-0.5f;
		transform.position = position;
	}
	public void setOrientation(Vector3 pos){
		//Vector3 position = pos;
		//position.y = a2.y;
		//Vector3 newPosition = new Vector3 (pos.x, , pos.z);
		this.gameObject.transform.LookAt (b1);

	}
	public void setLinesPosition(Vector3 a1p, Vector3 a2p, Vector3 b1p, Vector3 b2p){
		a1 = a1p;
		a2 = a2p;
		b1 = b1p;
		b2 = b2p;
	}
	public void setIds(int i1p, int i2p, int i3p, int i4p){
		i1 = i1p;
		i2 = i2p;
		i3 = i3p;
		i4 = i4p;
	}
//	void OnTriggerEnter(Collider collision)
//	{
//		if (collision.gameObject.tag == "Vehicle") {
//			collision.gameObject.GetComponent<MeshRenderer> ().enabled = false;
//		}
//	}
//	void OnTriggerExit(Collider other)
//	{
//		if (other.gameObject.tag == "Vehicle") {
//			other.gameObject.GetComponent<MeshRenderer> ().enabled = true;
//		}	
//	}
}
