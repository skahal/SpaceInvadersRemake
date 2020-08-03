using UnityEngine;
using System.Collections;
using Skahal.Tweening;
using Skahal.Camera;
using Skahal.ParticleSystems;

public class Ovni : MonoBehaviour
{
	private SpriteBuilder _spriteBuilder;
	private SpriteDestruction _spriteDestruction;
	private bool _canMove;
	private BoxCollider2D _collider;
	private AudioSource _audioSource;
	private LensFlare _lensFlare;
	private TrailRenderer _trail;

	public float DeployInterval = 12f;
	public float Speed = .1f;
	public AudioClip MoveSound;
	public AudioClip DieSound;
	public Transform VoyageStartPoint;
	public float DeployY;

	public static Ovni Instance;

	void Awake() { 
		Instance = this;
	}

	void Start ()
	{
		_spriteBuilder = GetComponentInChildren<SpriteBuilder> ();
		_spriteBuilder.Build ();

		_spriteDestruction = GetComponentInChildren<SpriteDestruction> ();
		_collider = GetComponent<BoxCollider2D> ();

		_audioSource = GetComponent<AudioSource> ();
		_lensFlare = GetComponent<LensFlare> ();
		_trail = GetComponentInChildren<TrailRenderer> ();

		StartCoroutine (Deploy ());
	}

	void FixedUpdate ()
	{
		if (_canMove && Cannon.Instance.CanInteract) {
			transform.position += new Vector3 (Speed, 0, 0);

			if ((Speed > 0 && transform.position.x > Game.Instance.RightBorder) 
		     || (Speed < 0 && transform.position.x < Game.Instance.LeftBorder)) {
				Redeploy ();
			}
		}
	}

	IEnumerator Deploy ()
	{	
		_collider.enabled = false;
		_canMove = false;
		_audioSource.Stop ();

		_spriteBuilder.Show ();
		_lensFlare.enabled = true;
		_trail.enabled = true;

		transform.position = GetCurrentVoyageStartPoint ();

		if (!Game.Instance.IsGameOver) {
			iTweenHelper.MoveTo (gameObject, 
				iT.MoveTo.position, GetCurrentVoyageEndPoint (),
				iT.MoveTo.time, DeployInterval,
				iT.MoveTo.easetype, iTween.EaseType.linear);
		}
		
		yield return new WaitForSeconds (DeployInterval * .9f);
	
		if (Cannon.Instance.CanInteract) {
			_audioSource.clip = MoveSound;
			_audioSource.Play ();

			_canMove = true;
			_collider.enabled = true;
		} else {
			Redeploy ();
		}
	}

	Vector3 GetCurrentVoyageStartPoint () {
		return VoyageStartPoint.transform.position;
	}

	Vector3 GetCurrentVoyageEndPoint() {
		float deployX = 0;

		if (Speed > 0) {
			deployX = Game.Instance.LeftBorder;
		} else {
			deployX = Game.Instance.RightBorder;
		}

		return new Vector3 (deployX, DeployY, 0);
	}

	void Redeploy() {
		_spriteBuilder.Hide ();
		_lensFlare.enabled = false;
		_trail.Reset (this);

		Speed *= -1f;
		StartCoroutine (Deploy ());
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		 if (other.IsProjectile ()) {
			_lensFlare.enabled = false;
			_trail.enabled = false;
			_audioSource.Stop ();
			_audioSource.PlayOneShot (DieSound);
			_canMove = false;
			Score.Instance.Sum (gameObject);

			Juiceness.Run ("OvniExplosion", () => {
				GetComponentInChildren<SpritePixel3DExplosion> ().Explode ();
			});
		
			StartCoroutine (_spriteDestruction.DestroySprite ());
		}
	}

	void OnSpriteDestructionEnd() {
		Redeploy ();
	}

	void OnGameOver() {
		_canMove = false;
		_audioSource.Stop ();
	}
}
