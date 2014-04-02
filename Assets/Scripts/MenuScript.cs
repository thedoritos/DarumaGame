using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

	[SerializeField]
	bool debugMode;

	public GUIStyle titleStyle;
	public GUIStyle defaultInteractiveStyle;
	public GUIStyle defaultStaticStyle;
	public GUIStyle gameClearStyle;
	public GUIStyle gameOverStyle;

	GameScript gameModel;
	
	// Use this for initialization
	void Start () {
		GameObject game = GameObject.Find("Game");
		gameModel = (GameScript) game.GetComponent(typeof(GameScript));
	}

	// Update is called once per frame
	void Update () {

	}

	void OnGUI () {
		// Forgive me for switching.
		// Since this is prototyping and the number fo the states is not too much.
		switch (gameModel.State) {
		case GameScript.GameState.TITLE:
			this.OnTitleGUI();
			break;
		case GameScript.GameState.PLAYING:
			this.OnPlayGUI();
			break;
		case GameScript.GameState.CLEARED:
		case GameScript.GameState.OVERED:
			this.OnResultGUI();
			break;
		}

		if (debugMode) this.OnDebugGUI ();
	}
	
	private void OnTitleGUI () {
		float mainPadding = 44.0f;
		Rect labelRect = new Rect(mainPadding, mainPadding + 60.0f, Screen.width - mainPadding * 2, 180.0f);
		Rect buttonRect = new Rect(mainPadding, Screen.height - (mainPadding + 180.0f), Screen.width - mainPadding * 2, 84.0f);

		GUI.Label(labelRect, "FIREMAN", titleStyle);

		if (GUI.Button(buttonRect, "Play", defaultInteractiveStyle)) {
			gameModel.StartGame();
		}
	}

	private void OnPlayGUI () {
		// Show hammer direction.
		Rect leftLabelRect = new Rect (22.0f, Screen.height * 0.5f - 44.0f, 44.0f, 44.0f);
		Rect rightLabelRect = new Rect (Screen.width - 66.0f, leftLabelRect.yMin, leftLabelRect.width, leftLabelRect.height);

		var leftLabelItem = ">>";
		var rightLabelItem = "<<";

		switch (gameModel.GetHammerPosition ()) {
		case GameScript.HammerSetPosition.LEFT:
			GUI.Label (leftLabelRect,  leftLabelItem,  defaultStaticStyle);
			GUI.Label (rightLabelRect, rightLabelItem, defaultInteractiveStyle);
			break;
		case GameScript.HammerSetPosition.RIGHT:
			GUI.Label (leftLabelRect,  leftLabelItem,  defaultInteractiveStyle);
			GUI.Label (rightLabelRect, rightLabelItem, defaultStaticStyle);
			break;
		default:
			GUI.Label (leftLabelRect,  leftLabelItem,  defaultStaticStyle);
			GUI.Label (rightLabelRect, rightLabelItem, defaultStaticStyle);
			break;
		}

		// Following UI will be implemented.
		// - Pause button
		// - Quit game button
		// - Resume game button
		// - Level information labels
	}

	private void OnResultGUI () {
		Rect resultLabelRect = new Rect(44.0f, Screen.height * 0.5f - 180.0f, Screen.width - 88.0f, 128.0f);

		switch (gameModel.State) {
		case GameScript.GameState.CLEARED:
			GUI.Label (resultLabelRect, "Clear!!!", gameClearStyle);
			break;
		case GameScript.GameState.OVERED:
			GUI.Label (resultLabelRect, "GameOver...", gameOverStyle);
			break;
		default:
			Debug.Log("Game is not finished.");
			break;
		}

		Rect retryButtonRect = new Rect(44.0f, Screen.height - 224.0f, Screen.width * 0.5f - 66.0f, 88.0f);
		Rect quitButtonRect   = new Rect(retryButtonRect.xMax + 44.0f, retryButtonRect.yMin, retryButtonRect.width, retryButtonRect.height);

		if (GUI.Button(retryButtonRect, "Retry", defaultInteractiveStyle)) {
			gameModel.StartGame();
		}
		if (GUI.Button(quitButtonRect, "Quit", defaultInteractiveStyle)) {
			gameModel.QuitGame();
		}
	}

	private void OnDebugGUI () {
		Rect resetButtonRect   = new Rect(22.0f, 22.0f, 88.0f, 88.0f);
		Rect restartButtonRect = new Rect(resetButtonRect.xMax + 22.0f, 22.0f, 88.0f, 88.0f);
		if (GUI.Button (resetButtonRect, "Exit")) {
			gameModel.QuitGame();
		}
		if (GUI.Button (restartButtonRect, "ReStart")) {
			gameModel.StartGame();
		}
	}
}
