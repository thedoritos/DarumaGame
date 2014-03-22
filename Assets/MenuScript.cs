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
		GUI.Box (new Rect (10, 10, 100, 90), "Reset");
		if (GUI.Button(new Rect(20, 40, 80, 20), "Application")) {
			Debug.Log("Reset Applicatioin");

			GameObject game = GameObject.Find("Game");
			GameScript gameModel = (GameScript) game.GetComponent(typeof(GameScript));
			gameModel.ResetAll();
		}
		if (GUI.Button(new Rect(20, 70, 80, 20), "Hammer")) {
			Debug.Log("Reset Hammer");

			GameObject game = GameObject.Find("Game");
			GameScript gameModel = (GameScript) game.GetComponent(typeof(GameScript));
			gameModel.ResetHammer();
		}
	}
}
