using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

	static float AccelerometerUpdateInterval = 1.0f / 60.0f;
	static float LowPassKernelWidthInSeconds = 1.0f;

	static float AccelerometerThreshold = 1.0f;

	float   LowPassFilterFactor = AccelerometerUpdateInterval / LowPassKernelWidthInSeconds;
	Vector3 lowPassValue = Vector3.zero;

	// Use this for initialization
	void Start () {
		lowPassValue = Input.acceleration;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI () {
		GUI.Box (new Rect (10, 10, 100, 90), "Application");
		if (GUI.Button(new Rect(20, 40, 80, 20), "Reset")) {
			Debug.Log("Level 1 Pressed");
		}
		if (GUI.Button(new Rect(20, 70, 80, 20), "Level 2")) {
			Debug.Log("Level 2 Pressed");
		}

		GUI.Box (new Rect (Screen.width - 110, 10, 100, 120), "Input");

		Vector3 lowPassedAccel = LowPassFilteredAcceleration ();

		GUI.Label (new Rect (Screen.width - 90,  40, 80, 20), "" + lowPassedAccel.x);
		GUI.Label (new Rect (Screen.width - 90,  70, 80, 20), "" + lowPassedAccel.y);
		GUI.Label (new Rect (Screen.width - 90, 100, 80, 20), "" + lowPassedAccel.z);
	}

	Vector3 LowPassFilteredAcceleration() {
		lowPassValue = Vector3.Lerp (lowPassValue, Input.acceleration, LowPassFilterFactor);
		return lowPassValue;
	}
}
