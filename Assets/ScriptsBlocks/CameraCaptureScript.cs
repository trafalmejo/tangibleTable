using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCaptureScript : MonoBehaviour {


	static WebCamTexture cameraBlocks;
	static WebCamDevice[] devices;
	// Use this for initialization
	void Start () {

		devices = WebCamTexture.devices;
		for (int i = 0; i < devices.Length; i++) {
			Debug.Log (devices[i].name);
		}
		if (cameraBlocks == null)
			cameraBlocks = new WebCamTexture ();

		cameraBlocks.deviceName = devices[1].name;

		GetComponent<Renderer> ().material.mainTexture = cameraBlocks;

		if (!cameraBlocks.isPlaying)
			cameraBlocks.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
