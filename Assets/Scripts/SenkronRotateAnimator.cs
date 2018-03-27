using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenkronRotateAnimator : Senkron {
	private int SHOW = 1;
	private int HIDE = 2;

	public int start, end;

	public int openAngle;
	public int showAngle;
	public int hideAngle;

	public int animRangeInFrame = 50;

	public GameObject pivotObject;

	private float angle;

	bool inAnim = false;
	bool isHiding = true;

	int movement = 0;
	float movementIndex = 0;

	// Use this for initialization
	void Start () {
		angle = openAngle;
	}
	
	// Update is called once per frame
	void Update () {
		if (movement == SHOW) {
			movementIndex += Mathf.PI / 2 / animRangeInFrame;

			angle = openAngle + Mathf.Abs (openAngle - showAngle) * Mathf.Sin(movementIndex);

			pivotObject.transform.localEulerAngles = new Vector3 (angle, 0, 0);

			if(movementIndex > Mathf.PI / 2){
				movementIndex = 0;
				movement = 0;
				inAnim = false;
				isHiding = false;
			}
		}
		else if(movement == HIDE){
			movementIndex += Mathf.PI / 2 / animRangeInFrame;

			angle = showAngle + Mathf.Abs (showAngle - hideAngle) * Mathf.Sin(movementIndex);

			pivotObject.transform.localEulerAngles = new Vector3 (angle, 0, 0);

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
		}
	}

	public override void hide(){
		if (!inAnim) {
			print ("Rotate anim: Hide");
			inAnim = true;
			movement = HIDE;
		}
	}
		
	override public void Sync(int globalIndex){
		if (globalIndex > start && globalIndex < end) {
			if (isHiding) {
				show ();
			}
		}
		else if(!isHiding){
			hide ();
		}
	}
}
