using UnityEngine;
using System.Collections;

public class Ovni : MonoBehaviour
{
	private SpriteBuilder m_spriteBuilder;
	private bool m_canMove;
	private BoxCollider2D m_collider;

	public float DeployInterval = 12f;
	public float Speed = .1f;

	void Awake ()
	{
		m_spriteBuilder = new SpriteBuilder (GetComponentInChildren<SpriteRenderer> ());
		m_spriteBuilder.Build ();

		m_collider = GetComponent<BoxCollider2D> ();

		StartCoroutine (Deploy ());
	}

	void Update ()
	{
		if (m_canMove && Cannon.Instance.CanInteract) {
			transform.position += new Vector3 (Speed, 0, 0);
		}
	}

	IEnumerator Deploy ()
	{	
		m_collider.enabled = false;
		m_spriteBuilder.Hide ();
		m_canMove = false;
		yield return new WaitForSeconds (DeployInterval);
	
		float x = 0;

		if (Speed > 0) {
			x = Game.Instance.LeftEdge.transform.position.x + 2;
		} else {
			x = Game.Instance.RightEdge.transform.position.x - 2;
		}

		transform.position = new Vector3 (x, transform.position.y, 0);
		m_spriteBuilder.Show ();
		m_canMove = true;
		m_collider.enabled = true;
	}

	void Redeploy() {
		Speed *= -1f;
		StartCoroutine (Deploy ());
	}

	void OnTriggerEnter2D (Collider2D collider)
	{
		if (collider.IsVerticalEdge ()) {
			Redeploy ();
		} else if (collider.IsProjectile ()) {
			Game.Instance.AddToScore (200);
			Redeploy ();
		}
	}
}
