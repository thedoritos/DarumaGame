using UnityEngine;
using System.Collections;

public class HammerScript : MonoBehaviour {

	public enum Direction {
		LEFT,
		RIGHT,
		NONE,
	}

	float forceAmount = 30.0f;

	[SerializeField]
	Vector3 rightInitPosition;

	[SerializeField]
	Vector3 leftInitPosition;

	Quaternion initRotation;

	// Use this for initialization
	void Start () {
		initRotation = this.transform.rotation;
		this.ResetPosition(Direction.RIGHT);
	}

	public void ResetPosition (Direction direction) {
		this.rigidbody.Sleep();
		switch (direction) {
			case Direction.LEFT:
				this.transform.position = leftInitPosition;
				Quaternion inverse = initRotation;
				inverse.y += 180.0f;
				this.transform.rotation = inverse;
			break;
			case Direction.RIGHT:
			default:
				this.transform.position = rightInitPosition;
				this.transform.rotation = initRotation;
			break;
		}
	}

	public Direction GetFacingDirection () {
//		float rotationY = this.transform.rotation.y;
//		if (rotationY > 0.5 && rotationY <= 1.5) {
//			return Direction.LEFT;
//		} else {
//			return Direction.RIGHT;
//		}
		if (this.transform.position == leftInitPosition) {
			return Direction.LEFT;
		} else if (this.transform.position == rightInitPosition) {
			return Direction.RIGHT;
		} else {
			return Direction.NONE;
		}
	}

	public void Swing (float accelerationX) {
		Vector3 force = Vector3.left;
		this.rigidbody.AddForce (force * accelerationX * forceAmount, ForceMode.VelocityChange);
	}
	
	// Update is called once per frame
	void Update () {

	}
}
