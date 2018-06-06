using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartSenkron : Touchable {
	public TouchableMultiVideoController videoController;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	override public void Touch(){
		videoController.Stop ();
	}
}
