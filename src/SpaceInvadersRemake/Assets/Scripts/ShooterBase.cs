using UnityEngine;
using System.Collections;

public abstract class ShooterBase: MonoBehaviour {
	protected Projectile Projectile;

	protected virtual void Awake() {
		SetupShooter ();
	}

	protected virtual void Update() {
		Shoot ();
	}

	void SetupShooter() {
		Projectile = transform.FindChild("Projectile") .GetComponent<Projectile> ();
		Projectile.transform.parent = null;
	}

	protected abstract bool CanShoot ();

	void Shoot() {
		if (CanShoot() && Cannon.Instance.CanInteract && !Projectile.IsMoving) {
			PerformShoot ();
		}
	}

	protected virtual void PerformShoot ()
	{
		Projectile.Shoot (transform.position.x, transform.position.y + (Projectile.Speed > 0 ? .5f : -.5f));
	}
}
