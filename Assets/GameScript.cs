using UnityEngine;
using System.Collections;

public class GameScript : MonoBehaviour {

	public enum HammerDirection {
		LEFT,
		RIGHT,
	}

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
		GameObject menu = GameObject.Find("Menu");
		MenuScript menuModel = (MenuScript) menu.GetComponent(typeof(MenuScript));
		menuModel.dismissDialog();

		GameObject darumaCreator = GameObject.Find("DarumaCreator");
		DarumaCreatorScript darumaCreatorScript = (DarumaCreatorScript)darumaCreator.GetComponent (typeof(DarumaCreatorScript));
		darumaCreatorScript.DestroyObjects();
		
		this.ResetHammer(HammerDirection.RIGHT);
		this.Setup();
	}

	public void ResetHammer(HammerDirection hammerDirection) {
		GameObject hammer = GameObject.Find("Hammer");
		HammerScript hammerModel = (HammerScript) hammer.GetComponent(typeof(HammerScript));

		switch (hammerDirection) {
		case HammerDirection.LEFT:
			hammerModel.ResetPosition(HammerScript.Direction.LEFT);
			break;
		case HammerDirection.RIGHT:
			hammerModel.ResetPosition(HammerScript.Direction.RIGHT);
			break;
		default:
			Debug.Log("Unexpected direction " + hammerDirection);
			break;
		}
	}

	// Update is called once per frame
	void Update () {
		// Check game clear & over.
		GameObject head = GameObject.FindWithTag("DarumaHead");
		DarumaHeadScript headModel = (DarumaHeadScript)head.GetComponent(typeof(DarumaHeadScript));
		if (headModel.OnFloor() && headModel.IsStopping()) {
			if (headModel.IsStanding()) {
				Debug.Log("Game Cleared !!!");
				
				GameObject menu = GameObject.Find("Menu");
				MenuScript menuModel = (MenuScript) menu.GetComponent(typeof(MenuScript));
				menuModel.showDialog("Finish", "Game Cleared !!!");
				
			} else {
				Debug.Log("Game Over...");
				
				GameObject menu = GameObject.Find("Menu");
				MenuScript menuModel = (MenuScript) menu.GetComponent(typeof(MenuScript));
				menuModel.showDialog("Finish", "Game Over...");
			}
		}

		// Input touches
		if (Input.touchCount == 1) {
			Touch touch = Input.GetTouch (0);
			if (touch.phase == TouchPhase.Began) {
				if (touch.position.x < Screen.width * 0.5f) {
					Debug.Log("Left side touched up.");
					this.ResetHammer(HammerDirection.LEFT);
				} else {
					Debug.Log("Right side touched up.");
					this.ResetHammer(HammerDirection.RIGHT);
				}
			}
		}

		// Input keys (for debug)
		if (Input.GetKeyUp ("l")) {
			this.ResetHammer(HammerDirection.RIGHT);
			return;
		} else if (Input.GetKeyUp("s")) {
			this.ResetHammer(HammerDirection.LEFT);
			return;
		} else if (Input.GetKeyUp("g")) {
			this.ResetAll();
			return;
		} else if (Input.GetKeyUp("j")) {
			return;
		} else if (Input.GetKeyUp("f")) {
			return;
		}

		// Input accelerations.
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
