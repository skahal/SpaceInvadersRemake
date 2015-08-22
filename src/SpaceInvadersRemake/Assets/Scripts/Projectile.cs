using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;
using Skahal.Camera;

public class Projectile: MonoBehaviour
{
	private Vector2? m_target;
	private BoxCollider2D m_collider;
	private SpriteRenderer m_renderer;
	private SpritePixel3DExplosion m_explosion;

	public float Speed = -0.05f;
	public string TargetTag = "Cannon";
	public float CameraShakeTime = .5f;
	public Vector3 CameraShakeAmount = new Vector3(.5f, .5f, .5f);

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
		m_collider = GetComponent<BoxCollider2D> ();
		m_renderer = GetComponentInChildren <SpriteRenderer> ();
		m_explosion = GetComponentInChildren<SpritePixel3DExplosion> ();
		gameObject.SetActive (false);
	}

	public void Shoot (float x, float y)
	{
		if (m_target == null) {
			m_target = new Vector2 (x, Speed > 0 ? 100 : -100);
			transform.position = new Vector2 (x, y);
			gameObject.SetActive (true);
			m_collider.enabled = true;
			m_renderer.enabled = true;
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
		if (other.CompareTag (TargetTag) || other.IsOvni()) {
			DestroyIt ();
			Juiceness.Run ("ProjectileCameraShake", () => {
				SHCameraHelper.Shake(CameraShakeTime, CameraShakeAmount);
			});
		}
		else if (other.IsHorizontalEdge ()) {
			DestroyIt ();
		}
	}

	public void DestroyIt ()
	{
		m_target = null;

		if (m_renderer.enabled & gameObject.activeInHierarchy) {
			Juiceness.Run ("ProjectileExplosion", () => {
				m_explosion.Explode ();
			});
		}

		m_collider.enabled = false;
		m_renderer.enabled = false;
	}
}
