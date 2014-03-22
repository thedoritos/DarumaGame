using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

	static float AccelerometerUpdateInterval = 1.0f / 60.0f;
	static float LowPassKernelWidthInSeconds = 1.0f;

	static float AccelerometerThreshold = 1.0f;

	float   LowPassFilterFactor = AccelerometerUpdateInterval / LowPassKernelWidthInSeconds;
	Vector3 lowPassValue = Vector3.zero;

	float hitAccel = 0.0f;
	bool  hitFlag  = false;
	int   hitDelay = 0;

	// Use this for initialization
	void Start () {
		lowPassValue = Input.acceleration;
		this.Setup();
	}

	void Setup() {
		GameObject darumaCreator = GameObject.Find("DarumaCreator");
		DarumaCreatorScript darumaCreatorScript = (DarumaCreatorScript)darumaCreator.GetComponent (typeof(DarumaCreatorScript));
		darumaCreatorScript.CreateObjects(7);
	}

	void Reset() {
		GameObject darumaCreator = GameObject.Find("DarumaCreator");
		DarumaCreatorScript darumaCreatorScript = (DarumaCreatorScript)darumaCreator.GetComponent (typeof(DarumaCreatorScript));
		darumaCreatorScript.DestroyObjects();

		GameObject hammer = GameObject.Find("Hammer");
		HammerScript hammerModel = (HammerScript) hammer.GetComponent(typeof(HammerScript));
		hammerModel.ResetPosition();

		this.Setup();
	}

	// Update is called once per frame
	void Update () {
		// TODO: think better implementation
		// + Not using flag variable
		// + Not getting object by name
		if (hitFlag && hitDelay == 0) {
			Debug.Log("Hit");

			GameObject hammer = GameObject.Find("Hammer");
			HammerScript hammerModel = (HammerScript) hammer.GetComponent(typeof(HammerScript));
			hammerModel.Swing(hitAccel);

			hitAccel = 0.0f;
			hitFlag  = false;
			hitDelay = 60;
		}

		hitDelay = (hitDelay <= 0) ? 0 : hitDelay - 1;
	}

	void OnGUI () {
		GUI.Box (new Rect (10, 10, 100, 90), "Reset");
		if (GUI.Button(new Rect(20, 40, 80, 20), "Application")) {
			Debug.Log("Reset Applicatioin");
			this.Reset();
		}
		if (GUI.Button(new Rect(20, 70, 80, 20), "Hammer")) {
			Debug.Log("Reset Hammer");
			GameObject hammer = GameObject.Find("Hammer");
			HammerScript hammerModel = (HammerScript) hammer.GetComponent(typeof(HammerScript));
			hammerModel.ResetPosition();
		}

		GUI.Box (new Rect (Screen.width - 110, 10, 100, 120), "Input");

		Vector3 inputAccel = Input.acceleration;
		Vector3 lowPassedAccel = LowPassFilteredAcceleration();

//		GUI.Label (new Rect (Screen.width - 90,  40, 80, 20), "" + lowPassedAccel.x);
//		GUI.Label (new Rect (Screen.width - 90,  70, 80, 20), "" + lowPassedAccel.y);
//		GUI.Label (new Rect (Screen.width - 90, 100, 80, 20), "" + lowPassedAccel.z);

//		Vector3 accel = Input.acceleration;
//		GUI.Label (new Rect (Screen.width - 90,  40, 80, 20), "" + accel.x);
//		GUI.Label (new Rect (Screen.width - 90,  70, 80, 20), "" + accel.y);
//		GUI.Label (new Rect (Screen.width - 90, 100, 80, 20), "" + accel.z);

		if (Mathf.Abs(inputAccel.x - lowPassedAccel.x) > AccelerometerThreshold) {
			hitAccel = inputAccel.x;
			hitFlag  = true;
		}
		GUI.Box (new Rect (Screen.width * 0.5f - 50, Screen.height * 0.5f - 30, 100, 60), "Accel");
		GUI.Label (new Rect (Screen.width * 0.5f - 40, Screen.height * 0.5f, 80, 20), "" + hitAccel);
	}

	Vector3 LowPassFilteredAcceleration() {
		lowPassValue = Vector3.Lerp (lowPassValue, Input.acceleration, LowPassFilterFactor);
		return lowPassValue;
	}
}
