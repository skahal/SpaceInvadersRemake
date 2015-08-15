using UnityEngine;
using System.Collections;

public class AudioRandomPitch : MonoBehaviour {

	private AudioSource m_audioSource;
	private float m_originalPitch;

	public bool RandomAtStart;
	public float Low = .95f;
	public float High = 1.05f;

	void Awake() {
		m_audioSource = GetComponent<AudioSource> ();
		m_originalPitch = m_audioSource.pitch;

		if (RandomAtStart) {
			RandomPitch ();
		}
	}

	void RandomPitch() {
		m_audioSource.pitch = Random.Range (Low, High);
	}
}
