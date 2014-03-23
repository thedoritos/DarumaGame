using UnityEngine;
using System.Collections;

public class DarumaHeadScript : MonoBehaviour {

	bool onFloor = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		// TODO: move to GameScript
		// because this is game logic not head logic
		if (OnFloor () && IsStopping () && IsStanding ()) {
			Debug.Log("Game Cleared !!!");
		}
	}

	bool OnFloor () {
		return onFloor;
	}

	bool IsStopping () {
		return this.rigidbody.velocity.magnitude < 0.01f;
	}

	bool IsStanding () {
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
