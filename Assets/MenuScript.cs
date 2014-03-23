using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

	string dialogTitle   = "";
	string dialogMessage = "";

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	void OnGUI () {

		// Show reset buttons.
		GUI.Box (new Rect (10, 10, 170, 74), "Reset");
		if (GUI.Button(new Rect(20, 30, 70, 44), "Game")) {
			Debug.Log("Reset Applicatioin");

			GameObject game = GameObject.Find("Game");
			GameScript gameModel = (GameScript) game.GetComponent(typeof(GameScript));
			gameModel.ResetAll();
		}
		if (GUI.Button(new Rect(100, 30, 70, 44), "Hammer")) {
			Debug.Log("Reset Hammer");

			GameObject game = GameObject.Find("Game");
			GameScript gameModel = (GameScript) game.GetComponent(typeof(GameScript));
			gameModel.ResetHammer();
		}

		// Show dialog.
		if (dialogTitle.Length != 0) {
			float width  = 120;
			float height = 120;
			GUI.Box(new Rect(Screen.width  * 0.5f - width  * 0.5f,
			                 Screen.height * 0.5f - height * 0.5f,
			                 width, height), dialogTitle);
			
			GUI.Label(new Rect(Screen.width  * 0.5f - width  * 0.5f + 10,
			                   Screen.height * 0.5f - height * 0.5f + 20,
			                   width - 20, height - 40), dialogMessage);
		}
	}

	public void showDialog(string title, string message) {
		dialogTitle = title;
		dialogMessage = message;
	}

	public void dismissDialog() {
		dialogTitle = "";
		dialogMessage = "";
	}
}
