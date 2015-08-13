using UnityEngine;
using System.Collections;

public class AudioRandomPitch : MonoBehaviour {

	public float Low = .95f;
	public float High = 1.05f;

	void Start() {
		var audioSource = GetComponent<AudioSource> ();
		audioSource.pitch = Random.Range (Low, High);
	}
}
