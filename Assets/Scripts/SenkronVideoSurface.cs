using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SenkronVideoSurface : MonoBehaviour {
	public VideoSurface surface;

	public int start;
	public int end;

	public RawImage rawImage;
	private Color color;

	bool isVisible = false;

	// Use this for initialization
	void Start () {
		color = new Color ();
		color = Color.white;
		color.a = 0f;

		rawImage = surface.GetComponent<RawImage> ();
		rawImage.color = color; 
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void visiblityWorker(bool desiredVisible){
		if (desiredVisible && !isVisible){
			color.a = color.a + 1f;
		}
		else if(!desiredVisible && isVisible){
			color.a = color.a - 10f;
		}

		isVisible = color.a >= 1f ? true : (color.a <= 0 ? false : isVisible);
		rawImage.color = color;
	}

	public void Sync(int globalIndex){
		if (globalIndex > start && globalIndex < end) {
			visiblityWorker (true);
			surface.GotoFrame (globalIndex - start); 			// localize global index to surface index 
		}
		else{
			visiblityWorker (false);
		}
	}
}
