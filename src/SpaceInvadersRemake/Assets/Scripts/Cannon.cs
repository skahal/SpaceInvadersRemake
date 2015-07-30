using UnityEngine;
using System.Collections;

public class Cannon: ShooterBase {

	private bool m_canMove = true;
	private SpriteRenderer m_renderer;
	public float Speed = .1f;
	public float SpawnTime = 1f;
	public float SpawnFlashTime = .15f;

	protected override void Awake ()
	{
		base.Awake ();
		m_renderer = GetComponentInChildren<SpriteRenderer> ();

		StartCoroutine (Spawn ());
	}

	private IEnumerator Spawn () {
		var flashes = SpawnTime / SpawnFlashTime;

		Debug.Log (flashes);
		for (int i = 0; i < flashes; i++) {
			m_renderer.enabled = !m_renderer.enabled;
			yield return new WaitForSeconds (SpawnFlashTime);
		}

		m_renderer.enabled = true;
	}

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
