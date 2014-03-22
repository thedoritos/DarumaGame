using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	void OnGUI () {
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
	}
}
