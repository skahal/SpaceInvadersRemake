using UnityEngine;

public class AudioRandomPitch : MonoBehaviour {

	private AudioSource _audioSource;

	public bool RandomAtStart;
	public float Low = .95f;
	public float High = 1.05f;

	void Awake() {
		_audioSource = GetComponent<AudioSource> ();

		if (RandomAtStart) {
			RandomPitch ();
		}
	}

	void RandomPitch() {
		_audioSource.pitch = Random.Range (Low, High);
	}
}
