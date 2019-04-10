using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntersectionSphere : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!GameManagerBlocks.instance.intersections.Contains (this.gameObject)) {
			//Destroy (this.gameObject);
			GameManagerBlocks.instance.deleteIntersection01(this.gameObject);

		}
	}
}
