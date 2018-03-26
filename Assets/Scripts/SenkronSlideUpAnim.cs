using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenkronSlideUpAnim : MonoBehaviour {
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

	int movement = 0;
	float movementIndex = 0;

	// Use this for initialization
	void Start () {
		Vector3 pos = obje.transform.localPosition;
		pos.y = hidePos;
		obje.transform.localPosition = pos;
	}

	// Update is called once per frame
	void Update () {
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
		}
	}

	public void hide(){
		if (!inAnim) {
			print ("Rotate anim: Hide");
			inAnim = true;
			movement = HIDE;
		}
	}

	public void Sync(int globalIndex){
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
