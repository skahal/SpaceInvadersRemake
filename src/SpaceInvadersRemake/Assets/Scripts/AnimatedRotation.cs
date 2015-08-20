using UnityEngine;
using System.Collections;

public class AnimatedRotation : MonoBehaviour {

	public Vector3 Rotation = Vector3.right;
	public float TimeToLife = 5f;

	void Start () {
		if (TimeToLife > 0) {
			StartCoroutine (StartTimeToLife ());
		}
	}

	IEnumerator StartTimeToLife() {
		yield return new WaitForSeconds (TimeToLife);
		Rotation = Vector3.zero;
	}

	void Update () {
		transform.Rotate(Rotation * Time.deltaTime);
	}
}
