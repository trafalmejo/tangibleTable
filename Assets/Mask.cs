using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mask : MonoBehaviour {

	public float height = 40.0f;
	public float diameter;
	// Use this for initialization
	void Start () {
		diameter = GameManagerBlocks.instance.diameter;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.M)) {
			increaseDiameter ();
		}
		if (Input.GetKeyDown (KeyCode.N)) {
			decreaseDiameter ();
		}		
		transform.position = new Vector3(transform.position.x, height, transform.position.z);
		transform.localScale = new Vector3 (diameter,0.1f,diameter);

	}
	public void increaseDiameter(){
		if (diameter < 5.0f) {
			diameter+= 0.05f;
			//transform.localScale =  
		}
	}
	public void decreaseDiameter(){
		if (diameter > 1.0f) {
			diameter-= 0.05f;
			//transform.localScale =  
		}
	}
}
