using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour {

	//THIS ELEMENT SHOULD BE KINECMATIC IN ORDER TO MOVE WITH OUT COLLISIONs

	private Type typeDummy;
	public float speed = 0.8f;
	private int nextPoint;
	private int cur;
	public int direction = 1;
	private GameObject[] objects;
	private LinkedList<GameObject> waypoints;
	private bool reset;
	private bool firstTime = true;

	// Use this for initialization
	void Start () {
		gameObject.tag = "Vehicle";
		reset = false;
		cur = 0;
		if (waypoints != null) {
			objects = new GameObject[waypoints.Count];
			int i = 0;
			foreach (GameObject point in waypoints) {
				objects [i] = point;
				i++;
			}
			if (objects [0] != null) {
				transform.position = objects [0].transform.position;
			}
		}
	}

	void FixedUpdate(){

		if (waypoints != null) {
			objects = new GameObject[waypoints.Count];
			int i = 0;
			foreach (GameObject point in waypoints) {
				objects [i] = point;
				i++;
			}
		}

		if (objects != null && cur < objects.Length) {
			//Debug.Log(cur);

				Vector3 target = objects [cur].transform.position;
			if (reset) {
				cur = 0;
				target = objects [0].transform.position;
				//Debug.Log (cur);
				reset = false;
			}	
				if (transform.position != target) {
					Vector3 p = Vector3.MoveTowards (transform.position,
						           target,
						           speed);
				
					//rotates
					transform.forward = Vector3.RotateTowards (transform.forward, target - transform.position, speed, 0.0f);
					//moves
					GetComponent<Rigidbody> ().MovePosition (p);
				}
		// Waypoint reached, select next one
		else {
				//cur = (cur + 1) % (objects.Length);
				if (cur >= 0 && cur < objects.Length) {
						cur = (cur + direction) % (objects.Length);					
				}
				if (cur < 0) {
					cur = 0;
				}
				if(objects.Length > 0){
				if (cur == objects.Length-1 || cur == 0) {
						direction = direction*-1;
				}
				}
			}
		}
}
	public void setWaypoints(LinkedList<GameObject> list){
		waypoints = list;
	}
	public void resetPosition(){
		reset = true;
		cur = 0;
	}

}
