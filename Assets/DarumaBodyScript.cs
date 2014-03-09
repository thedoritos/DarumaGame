using UnityEngine;
using System.Collections;

public class DarumaBodyScript : MonoBehaviour {

	float damp = 0.05f;

	// Use this for initialization
	void Start () {
	
	}

	void FixedUpdate () {
		Vector3 vel = rigidbody.velocity;
		vel.x *= 1 - damp;
		vel.y *= 1 - damp;
		vel.z *= 1 - damp;
		rigidbody.velocity = vel;
	}
	
	// Update is called once per frame
	void Update () {
//		this.FixedUpdate ();
	}
}
