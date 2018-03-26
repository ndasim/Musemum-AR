using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *	Created by ndasim, 06.03.2018 
 */
public class TabControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetMouseButtonDown(0))
		{
			Vector2 pos = Input.touchCount > 0 ? Input.GetTouch (0).position : new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
			Ray raycast = Camera.main.ScreenPointToRay(pos);
			RaycastHit raycastHit;
			if (Physics.Raycast(raycast, out raycastHit)){
				Debug.Log("Something Hit");

				Touchable touchableVideo = raycastHit.collider.GetComponent<Touchable> ();

				if (touchableVideo != null) {
					touchableVideo.Touch ();	
				}
			}
		}
	}
}