using UnityEngine;
using System.Collections;

public class DarumaCreatorScript : MonoBehaviour {

	public GameObject headPrefab;
	public GameObject bodyPrefab;
	public int level;

	public void CreateObjects () {
		this.CreateObjects(level);
	}

	public void CreateObjects (int level) {

		float bodyInterval = 0.21f;

		// Setup bodies.
		for (int i = 0; i < level; i++) {
			Vector3 pos = new Vector3(0.0f, bodyInterval * (i + 1), 0.0f);
			GameObject body = (GameObject) Instantiate(bodyPrefab, pos, new Quaternion());
			body.tag = "DarumaBody";
		}

		// Setup head.
		Vector3 headPos = new Vector3(0.0f, bodyInterval * (level + 2), 0.0f);
		GameObject head = (GameObject) Instantiate(headPrefab, headPos, new Quaternion());
		head.tag = "DarumaHead";
	}

	public void DestroyObjects() {

		// Destroy bodies.
		GameObject[] bodies = GameObject.FindGameObjectsWithTag("DarumaBody");
		foreach (GameObject body in bodies) {
			Destroy(body);
		}

		// Destroy head.
		GameObject head = GameObject.FindWithTag("DarumaHead");
		if (head != null) {
			Destroy(head);
		}
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

}
