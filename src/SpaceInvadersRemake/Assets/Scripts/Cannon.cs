using UnityEngine;
using System.Collections;

public class Cannon: ShooterBase {

	private bool m_canMove = true;
	public float Speed = 0.1f;

	protected override void Update ()
	{
		base.Update ();
		Move ();
	}

	void Move ()
	{
		var direction = (float)(Input.GetAxisRaw ("Horizontal"));
		var x = transform.position.x;

		if (!m_canMove && direction > 0 == x > 0)
			return;

		direction *= Speed;
		transform.position = new Vector3 (x + direction, transform.position.y);
	}

	protected override bool CanShoot ()
	{
		return Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown (KeyCode.X);
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.IsVerticalEdge()) {
			m_canMove = false;
		}
		else if (collider.IsProjectile()) {
			var projectile = collider.GetComponent<Projectile> ();

			if (projectile.IsTargetingCannon) {
				Die ();
			}
		}
	}

	void OnTriggerExit2D(Collider2D collider) {
		if (collider.IsVerticalEdge()) {
			m_canMove = true;
		}
	}

	void Die() {
		transform.position = new Vector2 (0, transform.position.y);
	}
}
