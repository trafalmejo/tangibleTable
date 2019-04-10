using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TUIO;


public class PointBlocks : MonoBehaviour {

	//represents the type of transports
	public Type type;
	public int idfiducial;
	private Vector3 lastPosition = Vector3.zero;
	private float threshold = 0.5f;
		
	// Update is called once per frame
	void Update () {

		Vector3 difference = transform.position - lastPosition;
		if (difference.x > threshold || difference.z > threshold)
		{
			//Debug.Log ("Point moved");
			if (type == Type.bus) {
				GameManagerBlocks.instance.roads [0].GetComponent<Road>().resetVehicle();
			}
			else if(type == Type.metrobus)
			{
				GameManagerBlocks.instance.roads [1].GetComponent<Road>().resetVehicle();
			}
			else if(type == Type.turibus){
				GameManagerBlocks.instance.roads [2].GetComponent<Road>().resetVehicle();

			}
			else if(type == Type.tramvia){
				GameManagerBlocks.instance.roads [3].GetComponent<Road>().resetVehicle();
			}
			else if(type == Type.bike){
				GameManagerBlocks.instance.roads [4].GetComponent<Road>().resetVehicle();
			}
			transform.hasChanged = false;
			//print("The transform has changed!");
			//transform.hasChanged = false;
		}
		lastPosition = transform.position;
		bool exists = GameManagerBlocks.instance.markerExists (idfiducial);
		//TUIO BOOL CONTROL IF THE BLOCK IS DELETED
		if(!exists && !GameManagerBlocks.instance.Tuio){
			GameManagerBlocks.instance.deletePoint (idfiducial);
		}
	}
	public void setId(int newid){
		idfiducial = newid;
	}
	public int getId(){
		return idfiducial;
	}
	public void setPosition(Vector3 newPosition){
		Vector3 p = idHeight(newPosition);
		transform.position = p;
	}
	public void setType(Type t){
		type = t;
	}
	public Vector3 idHeight(Vector3 pos){
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
}
