using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public float speed = 0.01f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		GameObject target = GameObject.FindWithTag("DarumaHead");
		if (target != null) {
			// Look at the direction of the head but not raise eyes.
			Quaternion cameraRotation = Quaternion.LookRotation(target.transform.position - this.transform.position);
			cameraRotation.x = -0.1f;
			this.transform.rotation = cameraRotation;
		}
	}
}
