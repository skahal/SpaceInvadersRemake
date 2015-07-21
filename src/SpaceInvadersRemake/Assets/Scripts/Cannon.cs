using UnityEngine;
using System.Collections;

public class Cannon: MonoBehaviour {

	private bool m_canMove = true;
	private Projectile m_projectile;
	public float Speed = 0.1f;
	public GameObject ProjectilePrefab;

	void Awake() {
		var goProjectile = Instantiate (ProjectilePrefab, transform.position, Quaternion.identity) as GameObject;
		m_projectile = goProjectile.GetComponent<Projectile> ();
		m_projectile.Setup (transform.position.y + 1);
	}

	void Update () {
		Move ();
		Fire ();
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

	void Fire() {
		if (Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown(KeyCode.X)) {
			m_projectile.Shoot (transform.position.x);
		}
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.tag == "VerticalEdge") {
			m_canMove = false;
		}
	}

	void OnTriggerExit2D(Collider2D collider) {
		if (collider.tag == "VerticalEdge") {
			m_canMove = true;
		}
	}
}
