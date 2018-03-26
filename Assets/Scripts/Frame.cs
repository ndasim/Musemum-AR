using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Frame : MonoBehaviour {
	Text viewer;
	bool run;
	// Use this for initialization
	void Start () {
		viewer = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void showframe(float framei){
		viewer.text = framei.ToString ();
	}
}
