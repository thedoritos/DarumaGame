﻿using UnityEngine;
using System.Collections;

public class HammerScript : MonoBehaviour {

	float forceAmount = 30.0f;

	Vector3 initPosition;
	Quaternion initRotation;

	// Use this for initialization
	void Start () {
		initPosition = this.transform.position;
		initRotation = this.transform.rotation;
	}

	public void ResetPosition () {
		this.rigidbody.Sleep();
		this.transform.position = initPosition;
		this.transform.rotation = initRotation;
	}

	public void Swing (float accelerationX) {
		Vector3 force = Vector3.left;
		this.rigidbody.AddForce (force * accelerationX * forceAmount, ForceMode.VelocityChange);
	}
	
	// Update is called once per frame
	void Update () {

		float value = Input.GetAxis ("LargeBang");

//		Debug.Log ("the input value = " + value);
//		if (value == 0.0) {
//			this.ResetPosition ();
//		} else {
//			Vector3 force = Vector3.left;
//			this.rigidbody.AddForce (force * forceAmount, ForceMode.VelocityChange);
//		}
	}
}
