using UnityEngine;
using System.Collections;

public class Cannon: ShooterBase {
	private bool _touchingEdge;
	private SpriteRenderer _renderer;
	private AudioSource _audioSource;

	public static Cannon Instance;
	public float Speed = .1f;
	public float SpawnTime = 1f;
	public float SpawnFlashTime = .15f;
	public int Lifes = 3;
	public AudioClip LoseLifeAudio;
	public AudioClip ShootAudio;
	public float ShootAudioVolume = .5f;

	[HideInInspector] public bool CanInteract;

	protected override void Awake () {
		Instance = this;
	
		base.Awake ();
		_renderer = GetComponentInChildren<SpriteRenderer> ();
		_audioSource = GetComponent<AudioSource> ();

		StartCoroutine (Spawn ());
	}

	private IEnumerator Spawn () {
		CanInteract = false;
		Game.Instance.RaiseMessage ("OnSpawnBegin");

		transform.position = Game.Instance.CannonDeployPosition;

		var flashes = SpawnTime / SpawnFlashTime;

		for (int i = 0; i < flashes; i++) {
			_renderer.enabled = !_renderer.enabled;
			yield return new WaitForSeconds (SpawnFlashTime);
		}

		_renderer.enabled = true;

		if (Lifes > 0) {
			CanInteract = true;
			Game.Instance.RaiseMessage ("OnSpawnEnd");
		} 
	}

	protected override void FixedUpdate ()
	{
		base.FixedUpdate ();
		Move ();
	}

	void Move ()
	{
		if (CanInteract) {
			transform.position = GetNewPosition (PlayerInput.Instance.HorizontalDirection);
		}
	}

	public Vector3 GetNewPosition(float horizontalDirection)
	{
		var direction = horizontalDirection * Time.deltaTime;
		var x = transform.position.x;

		// If is touching edge and is trying to move to edge direction again, 
		// abort the movement.
		if (_touchingEdge && direction > 0 == x > 0)
			return transform.position;

		direction *= Speed;

		return new Vector3 (x + direction, transform.position.y);
	}

	protected override bool CanShoot ()
	{
		return CanInteract && PlayerInput.Instance.IsShooting;
	}

	protected override void PerformShoot ()
	{
		base.PerformShoot ();
		SendMessage ("RandomPitch");
		_audioSource.PlayOneShot (ShootAudio, ShootAudioVolume);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.IsVerticalEdge()) {
			_touchingEdge = true;
		}
		else if (other.IsProjectile()) {
			var projectile = other.GetComponent<Projectile> ();

			if (projectile.IsTargetingCannon) {
				LoseLife ();
			}
		}
		else if (other.IsAlien ()) {
			Die ();
		}
	}

	void OnTriggerExit2D(Collider2D collider) {
		if (collider.IsVerticalEdge()) {
			_touchingEdge = false;
		}
	}

	void LoseLife(int lifesLost = 1) {
		Projectile.DestroyIt ();
		Lifes -= lifesLost;
		StartCoroutine (Spawn ());

		if (Lifes == 0) {
			Game.Instance.StartGameOver ();
		} else {
			_audioSource.PlayOneShot (LoseLifeAudio);
		}

	}
	public void Die() {		
		LoseLife (Lifes);
	}
}
