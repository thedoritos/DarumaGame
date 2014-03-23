using UnityEngine;
using System.Collections;

public class DarumaHeadScript : MonoBehaviour {

	bool onFloor = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	public bool OnFloor () {
		return onFloor;
	}

	public bool IsStopping () {
		return this.rigidbody.velocity.magnitude < 0.01f;
	}

	public bool IsStanding () {
		return Mathf.Abs (this.rigidbody.rotation.x) < 0.1f
			&& Mathf.Abs (this.rigidbody.rotation.z) < 0.1f;
	}

	void OnCollisionEnter (Collision collision) {
		GameObject obj = collision.gameObject;
		if (obj.name == "Floor") {
			Debug.Log("Head hits Floor");
			onFloor = true;
		} else {
			Debug.Log("Head hits Object name: " + collision.gameObject.name);
		}
	}
}
