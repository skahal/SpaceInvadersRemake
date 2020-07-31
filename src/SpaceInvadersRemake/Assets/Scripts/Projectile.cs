using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;
using Skahal.Camera;
using Skahal.ParticleSystems;
using Skahal.Threading;

public class Projectile: MonoBehaviour
{
	private Vector2? m_target;
	private BoxCollider2D m_collider;
	private SpriteRenderer m_renderer;
	private SpritePixel3DExplosion m_explosion;
	private TrailRenderer m_trail;
	private Light m_light;

	public float Speed = -0.05f;
	public string TargetTag = "Cannon";
	public float CameraShakeTime = .5f;
	public Vector3 CameraShakeAmount = new Vector3(.5f, .5f, .5f);
	public Color CameraFlashColor = Color.white;

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
		m_trail = GetComponentInChildren<TrailRenderer> ();
		m_light = GetComponentInChildren<Light> ();
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

			if (m_trail != null && Juiceness.CanRun("ProjectileTrail")) {
				m_trail.Reset (this);
				m_trail.enabled = true;
			}

			if(m_light != null && Juiceness.CanRun("ProjectileLight")) {
				m_light.enabled = true;
			}

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

			Juiceness.Run ("ProjectileCameraFlash", () => {
				Camera.main.backgroundColor = CameraFlashColor;
				SHCoroutine.StartEndOfFrame(() => {
					Camera.main.backgroundColor = Color.black;
				});
			});

			Game.Instance.RaiseMessage ("OnProjectileHit" + TargetTag, gameObject);
		}
		else if (other.IsHorizontalEdge ()) {
			DestroyIt ();
			Game.Instance.RaiseMessage ("OnProjectileMiss" + TargetTag, gameObject);
		}
		else if (other.IsProjectile ()) {
			Juiceness.Run ("ProjectileDetroyProjectile", () => {
				DestroyIt ();
			});

			Game.Instance.RaiseMessage ("OnProjectileMiss" + TargetTag, gameObject);
		}
	}

	public void DestroyIt ()
	{		
		if (m_renderer.enabled & gameObject.activeInHierarchy) {
			Juiceness.Run ("ProjectileExplosion", () => {
				m_explosion.Explode ();
			});
		}

		m_collider.enabled = false;
		m_renderer.enabled = false;

		if (m_trail != null) {
			m_trail.enabled = false;	
			m_light.enabled = false;
		}

		m_target = null;
	}
}
