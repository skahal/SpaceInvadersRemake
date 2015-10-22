using UnityEngine;
using System.Collections;

public class AlienBody : MonoBehaviour {
	private Alien m_alien;

	void Start() {
		m_alien = transform.parent.GetComponent<Alien> ();
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.IsProjectile ()) {
			var projectile = other.GetComponent<Projectile> ();

			if (projectile.IsTargetingAlien) {
				Score.Instance.Sum (gameObject, m_alien.Row * m_alien.RowScoreFactor);
				m_alien.Die ();
			}
		}
	}
}
