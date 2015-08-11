using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

public class Projectile: MonoBehaviour
{
	private Vector2? m_target;
	public float Speed = -0.05f;
	public string TargetTag = "Cannon";

	public bool IsTargetingCannon {
		get {
			return "Cannon".Equals (TargetTag);
		}
	}

	public bool IsTargetingAlien {
		get {
			return "Alien".Equals (TargetTag);
		}
	}

	public bool IsMoving { 
		get {
			return m_target != null;
		}
	}

	void Awake ()
	{
		gameObject.SetActive (false);
	}

	public void Shoot (float x, float y)
	{
		if (m_target == null) {
			m_target = new Vector2 (x, Speed > 0 ? 100 : -100);
			transform.position = new Vector2 (x, y);
			gameObject.SetActive (true);
		}
	}

	void FixedUpdate ()
	{
		if (m_target.HasValue && Cannon.Instance.CanInteract) {
			transform.position = Vector2.Lerp (transform.position, m_target.Value, Time.fixedDeltaTime * Mathf.Abs (Speed));
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.CompareTag (TargetTag)
		    || other.IsHorizontalEdge ()) {
			DestroyIt ();
		}
	}

	public void DestroyIt ()
	{
		m_target = null;
		gameObject.SetActive (false);
	}
}
