using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour {
	public GameObject path;
	public Transform[] points;
	public float reachDistance = 1;
	public float moveSpeed = 1;
	public float rotationSpeed = 1;

	Animator anim;

	public int pointIndex = 1;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		points = path.GetComponentsInChildren <Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 dif = transform.position - points [pointIndex].position;
		if (dif.magnitude > reachDistance) {

			//rotate us over time according to speed until we are in the required rotation
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(-dif.normalized), Time.deltaTime * rotationSpeed);

			transform.position += transform.forward * Time.deltaTime * moveSpeed;

			anim.SetInteger ("walk", 1);
		} else {
			pointIndex = ++pointIndex % (points.Length - 1);
		}
	}
}
