using UnityEngine;
using System.Collections;

public class Alien : MonoBehaviour {
	private AliensWave m_wave;

	void Start()
	{
		m_wave = gameObject.transform.parent.GetComponent<AliensWave> ();
	}

	void OnTriggerEnter2D(Collider2D collider) {
		Debug.Log ("collision" + collider.tag);

		if (collider.tag == "VerticalEdge") {
			m_wave.Flip ();
		}
	}
}
