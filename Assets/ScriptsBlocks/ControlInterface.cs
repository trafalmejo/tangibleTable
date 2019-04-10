using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlInterface : MonoBehaviour {


	public void debugBus(){
		GameManagerBlocks.instance.debugBus ();
	}
	public void debugMetroBus(){
		GameManagerBlocks.instance.debugMetroBus ();
	}
	public void debugTuriBus(){
		GameManagerBlocks.instance.debugTuriBus ();
	}
	public void debugTram(){
		GameManagerBlocks.instance.debugTram ();
				}
	public void debugBike(){
		GameManagerBlocks.instance.debugBike ();
	}}
