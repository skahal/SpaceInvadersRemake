using UnityEngine;
using System.Collections;

public class Alien : MonoBehaviour {
	private AliensWave m_wave;

	void Start()
	{
		m_wave = gameObject.transform.parent.GetComponent<AliensWave> ();
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.tag == "VerticalEdge") {
			m_wave.Flip ();
		}
		else if (collider.tag == "Projectile") {
			Die ();
		}
	}

	void Die() {
		gameObject.SetActive (false);
	}
}
