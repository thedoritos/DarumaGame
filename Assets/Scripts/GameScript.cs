using UnityEngine;
using System.Collections;

public class GameScript : MonoBehaviour {

	public enum GameState {
		TITLE,
		READY,
		PLAYING,
		PAUSING,
		CLEARED,
		OVERED,
	}

	public GameState State { get; private set;}

	public enum HammerSetPosition {
		LEFT,
		RIGHT,
		NONE,
	}

	[SerializeField]
	const int DefaultGameLevel = 7;

	[SerializeField]
	float AccelerometerThreshold = 1.0f;
	
	static float AccelerometerUpdateInterval = 1.0f / 60.0f;
	static float LowPassKernelWidthInSeconds = 1.0f;
	static float LowPassFilterFactor = AccelerometerUpdateInterval / LowPassKernelWidthInSeconds;

	Vector3 lowPassValue;
	
	float hitAccel = 0.0f;
	bool  hitFlag  = false;
	int   hitDelay = 0;

	HammerScript hammerModel;

	// Use this for initialization
	void Start () {
		// Keep references.
		GameObject hammer = GameObject.Find("Hammer");
		hammerModel = (HammerScript) hammer.GetComponent(typeof(HammerScript));

		// Setup values.
		lowPassValue = Input.acceleration;
	}

	//
	// GAME LIFECYCLES
	//
	public void StartGame(int gameLevel = DefaultGameLevel) {
		if (State == GameState.PLAYING) {
			Debug.Log("State already has been changed to " + State);
			return;
		}

		this.ResetDarumas(gameLevel);
		this.ResetHammer();

		State = GameState.READY;
	}

	public void PauseGame() {
		Debug.Log("Not implemented.");
	}

	public void ResumeGame() {
		Debug.Log("Not implemented.");
	}

	public void QuitGame() {
		if (State == GameState.TITLE) {
			Debug.Log("State already has been changed to " + State);
			return;
		}

		this.ResetDarumas(5);
		this.ResetHammer();

		State = GameState.TITLE;
	}

	// Update is called once per frame
	void Update () {

		// Ignore inputs (except GUI) if game is not started.
		if (State == GameState.TITLE) return;

		// Begin playing with READY.
		if (State == GameState.READY) {
			State = GameState.PLAYING;
			return;
		}

		// Update nothing if game is finished.
		if (State == GameState.CLEARED || State == GameState.OVERED) return;

		// Check game clear & over.
		// Reset references.
		GameObject head = GameObject.FindWithTag("DarumaHead");
		DarumaHeadScript headModel = (DarumaHeadScript)head.GetComponent(typeof(DarumaHeadScript));
		if (headModel.OnFloor() && headModel.IsStopping()) {
			if (headModel.IsStanding()) {
				Debug.Log("Game Cleared !!!");
				State = GameState.CLEARED;
			} else {
				Debug.Log("Game Over...");
				State = GameState.OVERED;
			}
		}

		if (headModel.OutOfScene ()) {
			Debug.Log("Game Over...");
			State = GameState.OVERED;
		}

		// Input touches
		if (Input.touchCount == 1) {
			Touch touch = Input.GetTouch (0);
			if (touch.phase == TouchPhase.Began) {
				if (touch.position.x < Screen.width * 0.5f) {
					this.ResetHammer(HammerScript.Direction.LEFT);
				} else {
					this.ResetHammer(HammerScript.Direction.RIGHT);
				}
			}
		}

		// Input keys (only for debug)
		if (Input.GetKeyUp("q")) {
			this.QuitGame();
			return;
		}

		// Input accelerations.
		Vector3 inputAccel = Input.acceleration;
		Vector3 lowPassedAccel = LowPassFilteredAcceleration();
		if (Mathf.Abs(inputAccel.x - lowPassedAccel.x) > AccelerometerThreshold) {

			if ((inputAccel.x < 0 && hammerModel.GetFacingDirection() == HammerScript.Direction.LEFT) ||
			    (inputAccel.x > 0 && hammerModel.GetFacingDirection() == HammerScript.Direction.RIGHT))
			{
				hitAccel = inputAccel.x;
				hitFlag  = true;
			}
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

	//
	// ACTIONS FOR GAME OBJECTS
	//
	public void SetHammer(HammerSetPosition position) {
		switch (position) {
		case HammerSetPosition.LEFT:
			this.ResetHammer(HammerScript.Direction.LEFT);
			break;
		case HammerSetPosition.RIGHT:
			this.ResetHammer(HammerScript.Direction.RIGHT);
			break;
		case HammerSetPosition.NONE:
			break;
		default:
			Debug.Log("Unexpected HammerSetPosition type " + position);
			break;
		}
	}

	public void SwingHammer(HammerSetPosition position, float accelerationX) {
		hammerModel.Swing(accelerationX);
	}

	public HammerSetPosition GetHammerPosition() {
		switch (hammerModel.GetFacingDirection ()) {
		case HammerScript.Direction.RIGHT:
			return HammerSetPosition.LEFT;
			break;
		case HammerScript.Direction.LEFT:
			return HammerSetPosition.RIGHT;
			break;
		case HammerScript.Direction.NONE:
		default:
			return HammerSetPosition.NONE;
			break;
		}
	}

	private void ResetDarumas(int gameLevel) {
		// Reset daruma objects.
		GameObject darumaCreator = GameObject.Find("DarumaCreator");
		DarumaCreatorScript darumaCreatorModel = (DarumaCreatorScript)darumaCreator.GetComponent (typeof(DarumaCreatorScript));
		darumaCreatorModel.DestroyObjects();
		darumaCreatorModel.CreateObjects(gameLevel);
	}

	private void ResetHammer(HammerScript.Direction hammerDirection = HammerScript.Direction.RIGHT) {
		hammerModel.ResetPosition(hammerDirection);
	}

	//
	// UTILITIES
	//

	// Remove noise from the acceleration input
	// by low-pass filtering it.
	private Vector3 LowPassFilteredAcceleration() {
		lowPassValue = Vector3.Lerp (lowPassValue, Input.acceleration, LowPassFilterFactor);
		return lowPassValue;
	}
}
