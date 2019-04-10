using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fenderrio.ImageWarp;
public class WarpControl : MonoBehaviour {

	public int pointToControl = 0;
	public ImageWarp imageWraper;
	public float change = 1.0f;
	// Use this for initialization
	void Start () {
		imageWraper = this.gameObject.GetComponent<ImageWarp> ();
		loadWarpPoints ();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("1")){
			pointToControl = 0;
		}
		if (Input.GetKeyDown("2")){
			pointToControl = 1;
		}
		if (Input.GetKeyDown("3")){
			pointToControl = 2;
		}
		if (Input.GetKeyDown("4")){
			pointToControl = 3;
		}
		if (Input.GetKeyDown(KeyCode.S)){
			saveWarpPoints ();
		}
		if (Input.GetKeyDown(KeyCode.L)){
			loadWarpPoints ();
		}		
		if (Input.GetKeyDown(KeyCode.R)){
			resetWarpPoints ();
		}
		//Debug.Log ("point control: " + pointToControl);
		if (pointToControl == 0) {
			if (Input.GetKeyDown(KeyCode.UpArrow)){
				//Debug.Log ("going up");
				float x = imageWraper.cornerOffsetTL.x;
				float y = imageWraper.cornerOffsetTL.y;
				float z = imageWraper.cornerOffsetTL.z;
				imageWraper.cornerOffsetTL = new Vector3 (x, y+change, z);
			}
			if (Input.GetKeyDown(KeyCode.DownArrow)){
				float x = imageWraper.cornerOffsetTL.x;
				float y = imageWraper.cornerOffsetTL.y;
				float z = imageWraper.cornerOffsetTL.z;
				imageWraper.cornerOffsetTL = new Vector3 (x, y-change, z);
			}
			if (Input.GetKeyDown(KeyCode.LeftArrow)){
				float x = imageWraper.cornerOffsetTL.x;
				float y = imageWraper.cornerOffsetTL.y;
				float z = imageWraper.cornerOffsetTL.z;
				imageWraper.cornerOffsetTL= new Vector3 (x-change, y, z);
			}
			if (Input.GetKeyDown(KeyCode.RightArrow)){
				float x = imageWraper.cornerOffsetTL.x;
				float y = imageWraper.cornerOffsetTL.y;
				float z = imageWraper.cornerOffsetTL.z;
				imageWraper.cornerOffsetTL= new Vector3 (x+change, y, z);
			}
		}
		else if (pointToControl == 1) {
			if (Input.GetKeyDown(KeyCode.UpArrow)){
				float x = imageWraper.cornerOffsetTR.x;
				float y = imageWraper.cornerOffsetTR.y;
				float z = imageWraper.cornerOffsetTR.z;
				imageWraper.cornerOffsetTR= new Vector3 (x, y+change, z);
			}
			if (Input.GetKeyDown(KeyCode.DownArrow)){
				float x = imageWraper.cornerOffsetTR.x;
				float y = imageWraper.cornerOffsetTR.y;
				float z = imageWraper.cornerOffsetTR.z;
				imageWraper.cornerOffsetTR= new Vector3 (x, y-change, z);
			}
			if (Input.GetKeyDown(KeyCode.LeftArrow)){
				float x = imageWraper.cornerOffsetTR.x;
				float y = imageWraper.cornerOffsetTR.y;
				float z = imageWraper.cornerOffsetTR.z;
				imageWraper.cornerOffsetTR= new Vector3 (x-change, y, z);
			}
			if (Input.GetKeyDown(KeyCode.RightArrow)){
				float x = imageWraper.cornerOffsetTR.x;
				float y = imageWraper.cornerOffsetTR.y;
				float z = imageWraper.cornerOffsetTR.z;
				imageWraper.cornerOffsetTR= new Vector3 (x+change, y, z);
			}
		}
		else if (pointToControl == 2) {
			if (Input.GetKeyDown(KeyCode.UpArrow)){
				float x = imageWraper.cornerOffsetBR.x;
				float y = imageWraper.cornerOffsetBR.y;
				float z = imageWraper.cornerOffsetBR.z;
				imageWraper.cornerOffsetBR= new Vector3 (x, y+change, z);
			}
			if (Input.GetKeyDown(KeyCode.DownArrow)){
				float x = imageWraper.cornerOffsetBR.x;
				float y = imageWraper.cornerOffsetBR.y;
				float z = imageWraper.cornerOffsetBR.z;
				imageWraper.cornerOffsetBR= new Vector3 (x, y-change, z);
			}
			if (Input.GetKeyDown(KeyCode.LeftArrow)){
				float x = imageWraper.cornerOffsetBR.x;
				float y = imageWraper.cornerOffsetBR.y;
				float z = imageWraper.cornerOffsetBR.z;
				imageWraper.cornerOffsetBR= new Vector3 (x-change, y, z);
			}
			if (Input.GetKeyDown(KeyCode.RightArrow)){
				float x = imageWraper.cornerOffsetBR.x;
				float y = imageWraper.cornerOffsetBR.y;
				float z = imageWraper.cornerOffsetBR.z;
				imageWraper.cornerOffsetBR= new Vector3 (x+change, y, z);
			}
		}
		else if (pointToControl == 3) {
			if (Input.GetKeyDown(KeyCode.UpArrow)){
				float x = imageWraper.cornerOffsetBL.x;
				float y = imageWraper.cornerOffsetBL.y;
				float z = imageWraper.cornerOffsetBL.z;
				imageWraper.cornerOffsetBL= new Vector3 (x, y+change, z);
			}
			if (Input.GetKeyDown(KeyCode.DownArrow)){
				float x = imageWraper.cornerOffsetBL.x;
				float y = imageWraper.cornerOffsetBL.y;
				float z = imageWraper.cornerOffsetBL.z;
				imageWraper.cornerOffsetBL= new Vector3 (x, y-change, z);
			}
			if (Input.GetKeyDown(KeyCode.LeftArrow)){
				float x = imageWraper.cornerOffsetBL.x;
				float y = imageWraper.cornerOffsetBL.y;
				float z = imageWraper.cornerOffsetBL.z;
				imageWraper.cornerOffsetBL= new Vector3 (x-change, y, z);
			}
			if (Input.GetKeyDown(KeyCode.RightArrow)){
				float x = imageWraper.cornerOffsetBL.x;
				float y = imageWraper.cornerOffsetBL.y;
				float z = imageWraper.cornerOffsetBL.z;
				imageWraper.cornerOffsetBL= new Vector3 (x+change, y, z);
			}
		}

	}
	public void saveWarpPoints(){
		PlayerPrefs.SetFloat ("XcornerTL", imageWraper.cornerOffsetTL.x);
		PlayerPrefs.SetFloat ("YcornerTL", imageWraper.cornerOffsetTL.y);
		PlayerPrefs.SetFloat ("XcornerTR", imageWraper.cornerOffsetTR.x);
		PlayerPrefs.SetFloat ("YcornerTR", imageWraper.cornerOffsetTR.y);
		PlayerPrefs.SetFloat ("XcornerBL", imageWraper.cornerOffsetBL.x);
		PlayerPrefs.SetFloat ("YcornerBL", imageWraper.cornerOffsetBL.y);
		PlayerPrefs.SetFloat ("XcornerBR", imageWraper.cornerOffsetBR.x);
		PlayerPrefs.SetFloat ("YcornerBR", imageWraper.cornerOffsetBR.y);

	}
	public void loadWarpPoints(){
		float xtl = PlayerPrefs.GetFloat ("XcornerTL");
		float ytl = PlayerPrefs.GetFloat ("YcornerTL");
		float xtr = PlayerPrefs.GetFloat ("XcornerTR");
		float ytr = PlayerPrefs.GetFloat ("YcornerTR");
		float xbl = PlayerPrefs.GetFloat ("XcornerBL");
		float ybl = PlayerPrefs.GetFloat ("YcornerBL");
		float xbr = PlayerPrefs.GetFloat ("XcornerBR");
		float ybr = PlayerPrefs.GetFloat ("YcornerBR");
		imageWraper.cornerOffsetTL= new Vector3 (xtl, ytl, 0.0f);
		imageWraper.cornerOffsetTR= new Vector3 (xtr, ytr, 0.0f);
		imageWraper.cornerOffsetBL= new Vector3 (xbl, ybl, 0.0f);
		imageWraper.cornerOffsetBR= new Vector3 (xbr, ybr, 0.0f);

	}
	public void resetWarpPoints(){
		imageWraper.cornerOffsetTL= new Vector3 (0.0f, 0.0f, 0.0f);
		imageWraper.cornerOffsetTR= new Vector3 (0.0f, 0.0f, 0.0f);
		imageWraper.cornerOffsetBL= new Vector3 (0.0f, 0.0f, 0.0f);
		imageWraper.cornerOffsetBR= new Vector3 (0.0f, 0.0f, 0.0f);
	}
}
