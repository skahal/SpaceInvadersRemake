using UnityEngine;
using System.Collections;

public class Cannon: MonoBehaviour {

	private bool m_canMove = true;
	public float Speed = 0.1f;

	void Update () {
		var direction = (float)(Input.GetAxisRaw ("Horizontal"));
		var x = transform.position.x;

		if (!m_canMove && direction > 0 == x > 0)
			return;
		
		direction *= Speed;
		
		transform.position = new  Vector3 (x + direction, transform.position.y);
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
