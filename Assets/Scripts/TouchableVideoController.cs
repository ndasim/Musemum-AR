using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchableVideoController : Touchable {
	public VideoSurface surface;
	public RawImage controller;
	public BoxCollider boxCollider;

	
	// Update is called once per frame
	void Update () {
		controller.enabled = boxCollider.enabled && !surface.playing;
	}


	public void Play(){
		surface.Play ();
		controller.enabled = false;
	}


	public void Pause(){
		surface.Pause ();
		controller.enabled = true;
	}


	override public void Touch(){
		if (surface.playing) {
			Pause ();
		}
		else{
			Play ();
		}
	}
}
