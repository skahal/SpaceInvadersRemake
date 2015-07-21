using UnityEngine;
using System.Collections;

public class Projectile: MonoBehaviour {
	private Vector2? m_target;
	private float m_shootFromY;
	public float Speed = .1f;

	public void Setup(float shootFromY)
	{
		m_shootFromY = shootFromY;
		gameObject.SetActive (false);
	}

	public void Shoot(float x) {
		if (m_target == null) {
			m_target = new Vector2 (x, 100);
			transform.position = new Vector2 (x, m_shootFromY);
			gameObject.SetActive (true);
		}
	}

	void Update () {
		if (m_target.HasValue) {
			transform.position = Vector2.Lerp (transform.position, m_target.Value, Time.deltaTime * Speed);
		}
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.tag == "Alien") {
			DestroyIt ();
		} else if (collider.tag == "HorizontalEdge") {
			DestroyIt ();
		}
	}

	void DestroyIt() {
		m_target = null;
		gameObject.SetActive (false);
	}
}
