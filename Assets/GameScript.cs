using UnityEngine;
using System.Collections;

public class GameScript : MonoBehaviour {

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

	public void Setup() {
		GameObject darumaCreator = GameObject.Find("DarumaCreator");
		DarumaCreatorScript darumaCreatorScript = (DarumaCreatorScript)darumaCreator.GetComponent (typeof(DarumaCreatorScript));
		darumaCreatorScript.CreateObjects(7);
	}

	public void ResetAll() {
		GameObject darumaCreator = GameObject.Find("DarumaCreator");
		DarumaCreatorScript darumaCreatorScript = (DarumaCreatorScript)darumaCreator.GetComponent (typeof(DarumaCreatorScript));
		darumaCreatorScript.DestroyObjects();
		
		this.ResetHammer();
		this.Setup();
	}

	public void ResetHammer() {
		GameObject hammer = GameObject.Find("Hammer");
		HammerScript hammerModel = (HammerScript) hammer.GetComponent(typeof(HammerScript));
		hammerModel.ResetPosition();
	}
	
	// Update is called once per frame
	void Update () {

		// Update acceleration input.
		Vector3 inputAccel = Input.acceleration;
		Vector3 lowPassedAccel = LowPassFilteredAcceleration();
		if (Mathf.Abs(inputAccel.x - lowPassedAccel.x) > AccelerometerThreshold) {
			hitAccel = inputAccel.x;
			hitFlag  = true;
		}

		// Trigger Hammer Action.
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
	
	Vector3 LowPassFilteredAcceleration() {
		lowPassValue = Vector3.Lerp (lowPassValue, Input.acceleration, LowPassFilterFactor);
		return lowPassValue;
	}
}
