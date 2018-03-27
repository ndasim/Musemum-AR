using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenkronSlideAnim : Senkron {
	private int SHOW = 1;
	private int HIDE = 2;

	public int start, end;

	public Vector3 targetPosition;
	private Vector3 initialPosition;
	private Vector3 currentPosition;

	public int animRangeInFrame = 50;

	public GameObject obje;

	bool inAnim = false;
	bool isWaiting = true;

	int movement = 0;
	float movementIndex = 0;

	// Use this for initialization
	void Start () {
		initialPosition = obje.transform.localPosition;
	}

	// Update is called once per frame
	void Update () {
		if (movement == SHOW) {
			movementIndex += Mathf.PI / 2 / animRangeInFrame;

			currentPosition = obje.transform.localPosition;

			currentPosition.x = initialPosition.x + (targetPosition.x - initialPosition.x) * Mathf.Sin(movementIndex);
			currentPosition.y = initialPosition.y + (targetPosition.y - initialPosition.y) * Mathf.Sin(movementIndex);
			currentPosition.z = initialPosition.z + (targetPosition.z - initialPosition.z) * Mathf.Sin(movementIndex);

			obje.transform.localPosition = currentPosition;

			if(movementIndex > Mathf.PI / 2){
				movementIndex = 0;
				movement = 0;
				inAnim = false;
				isWaiting = false;
			}
		}
	}

	public void act (){
		if (!inAnim) {
			initialPosition = obje.transform.localPosition;
			print ("Rotate anim: Show");
			inAnim = true;
			movement = SHOW;
			isWaiting = false;
		}
	}

	public override void hide(){
	
	}

	public override void Sync(int globalIndex){
		if (globalIndex > start) {
			if (isWaiting) {
				act ();
			}
		}
	}
}
