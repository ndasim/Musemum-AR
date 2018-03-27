using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenkronSlideUpAnim : Senkron {
	private int SHOW = 1;
	private int HIDE = 2;

	public int start, end;

	public float hidePos;
	public float showPos;

	public int animRangeInFrame = 50;

	public GameObject obje;

	private float ypos;

	bool inAnim = false;
	bool isHiding = true;
	bool syncing = false;

	int movement = 0;
	float movementIndex = 0;

	MeshRenderer[] childMeshes;

	// Use this for initialization
	void Start () {
		childMeshes = obje.GetComponentsInChildren<MeshRenderer> ();

		Vector3 pos = obje.transform.localPosition;
		pos.y = hidePos;
		obje.transform.localPosition = pos;

		foreach (MeshRenderer meshrenderer in childMeshes) {
			meshrenderer.enabled = false;
		}
	}

	// Update is called once per frame
	void Update () {
		if (!syncing) {
			foreach (MeshRenderer meshrenderer in childMeshes) {
				meshrenderer.enabled = false;
			}
		}

		if (movement == SHOW) {
			movementIndex += Mathf.PI / 2 / animRangeInFrame;

			ypos = hidePos + (showPos - hidePos) * Mathf.Sin(movementIndex);

			Vector3 pos = obje.transform.localPosition;
			pos.y = ypos; 
			obje.transform.localPosition = pos;

			if(movementIndex > Mathf.PI / 2){
				movementIndex = 0;
				movement = 0;
				inAnim = false;
				isHiding = false;
			}
		}
		else if(movement == HIDE){
			movementIndex += Mathf.PI / 2 / animRangeInFrame;

			ypos = showPos - (showPos - hidePos) * Mathf.Sin(movementIndex);

			Vector3 pos = obje.transform.localPosition;
			pos.y = ypos; 
			obje.transform.localPosition = pos;

			if(movementIndex > Mathf.PI / 2){
				movementIndex = 0;
				movement = 0;
				inAnim = false;
				isHiding = true;
			}
		}
	}

	public void show (){
		if (!inAnim) {
			print ("Rotate anim: Show");
			inAnim = true;
			movement = SHOW;

			foreach (MeshRenderer meshrenderer in childMeshes) {
				meshrenderer.enabled = true;
			}
		}
	}

	public override void hide(){
		if (!inAnim) {
			//print ("Rotate anim: Hide");
			inAnim = true;
			movement = HIDE;
		}

		foreach (MeshRenderer meshrenderer in childMeshes) {
			meshrenderer.enabled = false;
		}
	}

	override public void Sync(int globalIndex){
		syncing = true;
		if (globalIndex > start && globalIndex < end) {
			if (isHiding) {
				show ();
			}
		} else if (!isHiding) {
			hide ();
		} else {
			foreach (MeshRenderer meshrenderer in childMeshes) {
				meshrenderer.enabled = false;
			}
		}
	}
}
