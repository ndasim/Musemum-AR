using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContiniousRotate : MonoBehaviour {
	float i;
	public int speed;

	// Use this for initialization
	void Start () {
		i = transform.rotation.y;
	}

	// Update is called once per frame
	void Update () {
		i += speed * Time.deltaTime;
		transform.localEulerAngles = new Vector3 (0, i, 0);
	}
}
