using UnityEngine;
using System.Collections;

public abstract class ShooterBase: MonoBehaviour {
	private Projectile m_projectile;

	protected virtual void Awake() {
		SetupShooter ();
	}

	protected virtual void Update() {
		Shoot ();
	}

	void SetupShooter() {
		m_projectile = transform.FindChild("Projectile") .GetComponent<Projectile> ();
		m_projectile.transform.parent = null;
		m_projectile.Setup (transform.position.y + (m_projectile.Speed > 0 ? 1 : -1));
	}

	protected abstract bool CanShoot ();

	void Shoot() {
		if (CanShoot()) {
			m_projectile.Shoot (transform.position.x);
		}
	}
}
