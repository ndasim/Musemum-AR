using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vuforia;

public class LoadDetect : MonoBehaviour {
	int wait = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (SceneManager.GetActiveScene ().name == "AudioTour") 
		{
			if (wait == 250) {
				
			}
			if (wait++ == 50) {
				VuforiaAbstractConfiguration.Instance.Vuforia.DelayedInitialization = false;
				VuforiaRuntime.Instance.InitVuforia ();
				GetComponent<VuforiaBehaviour> ().enabled = true;

			}	
		}
	}
}
