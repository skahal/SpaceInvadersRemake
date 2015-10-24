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
				Score.Instance.Sum (m_alien.gameObject);
				m_alien.Die ();
			}
		}
		else if (other.IsCannonZone ()) {
			Cannon.Instance.Die ();
		}
	}
}
