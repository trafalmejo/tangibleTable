using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour {

	public float x;
	public float y;
	public float z;

	// Use this for initialization
	void Start () {
		x = Random.Range (-120.0f, 120.0f);
		z = Random.Range (-60.0f, 60.0f);
		positionCalculate();
		//Instantiate(gameObject, new Vector3(x, 0.0f, z), Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.T)) {
			positionCalculate();
		}
	}
	void positionCalculate(){
		x = Random.Range (-120.0f, 120.0f);
		z = Random.Range (-60.0f, 60.0f);
		gameObject.transform.position = new Vector3 (x, 0.0f, z);
	}

	void OnTriggerEnter(Collider other)
	{
		//positionCalculate();
		//Debug.Log (other.gameObject.name);
	}
}
