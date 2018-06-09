using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasVisibilty : MonoBehaviour {

	BoxCollider collider;
	Canvas canvas;

	// Use this for initialization
	void Start () {
		collider = gameObject.AddComponent<BoxCollider>() as BoxCollider;
		canvas = GetComponent<Canvas> ();
	}

	// Update is called once per frame
	void Update () {
		canvas.enabled = collider.enabled;
	}
}
