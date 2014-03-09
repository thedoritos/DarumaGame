using UnityEngine;
using System.Collections;

public class DarumaCreatorScript : MonoBehaviour {

	public GameObject headPrefab;
	public GameObject bodyPrefab;
	public int level;

	void SetupObjects (int level) {

		float bodyInterval = 0.21f;

		// Setup bodies
		for (int i = 0; i < level; i++) {
			Vector3 pos = new Vector3(0.0f, bodyInterval * (i + 1), 0.0f);
			Instantiate(bodyPrefab, pos, new Quaternion());
		}

		// Setup head
		Vector3 headPos = new Vector3(0.0f, bodyInterval * (level + 2), 0.0f);
		Instantiate(headPrefab, headPos, new Quaternion());
	}

	// Use this for initialization
	void Start () {
		Debug.Log ("Create Darumas for level \'" + level + "\'.");
		this.SetupObjects (level);
	}
	
	// Update is called once per frame
	void Update () {
//		if (Input.GetButtonUp ("Jump")) {
//			Debug.Log("Create object from prefab \"" + bodyPrefab.name + "\".");
//
//			Instantiate(bodyPrefab, this.transform.position, this.transform.rotation);
//			this.transform.Translate(0.0f, 0.1f, 0.0f);
//		}
	}

}
