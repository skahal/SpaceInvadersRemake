using UnityEngine;
using Skahal.Camera;
using Skahal.ParticleSystems;
using Skahal.Threading;

public class Projectile: MonoBehaviour
{
	private Vector2? _target;
	private BoxCollider2D _collider;
	private SpriteRenderer _renderer;
	private SpritePixel3DExplosion _explosion;
	private TrailRenderer _trail;
	private Light _light;

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
			return _target != null;
		}
	}

	void Awake ()
	{
		_collider = GetComponent<BoxCollider2D> ();
		_renderer = GetComponentInChildren <SpriteRenderer> ();
		_explosion = GetComponentInChildren<SpritePixel3DExplosion> ();
		_trail = GetComponentInChildren<TrailRenderer> ();
		_light = GetComponentInChildren<Light> ();
		gameObject.SetActive (false);
	}

	public void Shoot (float x, float y)
	{
		if (_target == null) {
			_target = new Vector2 (x, Speed > 0 ? 100 : -100);
			transform.position = new Vector2 (x, y);
			gameObject.SetActive (true);
			_collider.enabled = true;
			_renderer.enabled = true;

			if (_trail != null) {
				_trail.Reset (this);
				_trail.enabled = true;
			}

			if(_light != null) {
				_light.enabled = true;
			}

		}
	}

	void FixedUpdate ()
	{
		if (_target.HasValue && Cannon.Instance.CanInteract) {
			transform.position = Vector2.Lerp (transform.position, _target.Value, Time.fixedDeltaTime * Mathf.Abs (Speed));
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.CompareTag (TargetTag) || other.IsOvni()) {
			DestroyIt ();

			SHCameraHelper.Shake(CameraShakeTime, CameraShakeAmount);
			
			Camera.main.backgroundColor = CameraFlashColor;
			SHCoroutine.StartEndOfFrame(() => {
				Camera.main.backgroundColor = Color.black;
			});			

			Game.Instance.RaiseMessage ("OnProjectileHit" + TargetTag, gameObject);
		}
		else if (other.IsHorizontalEdge ()) {
			DestroyIt ();
			Game.Instance.RaiseMessage ("OnProjectileMiss" + TargetTag, gameObject);
		}
		else if (other.IsProjectile ()) {
			DestroyIt ();

			Game.Instance.RaiseMessage ("OnProjectileMiss" + TargetTag, gameObject);
		}
	}

	public void DestroyIt ()
	{		
		if (_renderer.enabled & gameObject.activeInHierarchy) {
			_explosion.Explode();
		}

		_collider.enabled = false;
		_renderer.enabled = false;

		if (_trail != null) {
			_trail.enabled = false;	
			_light.enabled = false;
		}

		_target = null;
	}
}
