using UnityEngine;
using System.Collections;
using Skahal.Tweening;
using Skahal.Camera;
using Skahal.ParticleSystems;

public class Ovni : MonoBehaviour
{
	private SpriteBuilder m_spriteBuilder;
	private SpriteDestruction m_spriteDestruction;
	private bool m_canMove;
	private BoxCollider2D m_collider;
	private AudioSource m_audioSource;
	private LensFlare m_lensFlare;
	private TrailRenderer m_trail;
	private Vector3 m_currentVoyageStartPoint;
	private Vector3 m_currentVoyageEndPoint;

	public float DeployInterval = 12f;
	public float Speed = .1f;
	public AudioClip MoveSound;
	public AudioClip DieSound;
	public Transform VoyageStartPoint;
	public float DeployY;

	void Start ()
	{
		m_spriteBuilder = GetComponentInChildren<SpriteBuilder> ();
		m_spriteBuilder.Build ();

		m_spriteDestruction = GetComponentInChildren<SpriteDestruction> ();
		m_collider = GetComponent<BoxCollider2D> ();

		m_audioSource = GetComponent<AudioSource> ();
		m_lensFlare = GetComponent<LensFlare> ();
		m_trail = GetComponentInChildren<TrailRenderer> ();

		StartCoroutine (Deploy ());
	}

	void FixedUpdate ()
	{
		if (m_canMove && Cannon.Instance.CanInteract) {
			transform.position += new Vector3 (Speed, 0, 0);

			if ((Speed > 0 && transform.position.x > Game.Instance.RightBorder) 
		     || (Speed < 0 && transform.position.x < Game.Instance.LeftBorder)) {
				Redeploy ();
			}
		}
	}

	IEnumerator Deploy ()
	{	
		m_collider.enabled = false;
		m_canMove = false;
		m_audioSource.Stop ();

		m_spriteBuilder.Show ();
		m_lensFlare.enabled = true;
		m_trail.enabled = true;

		transform.position = GetCurrentVoyageStartPoint ();

		iTweenHelper.MoveTo (gameObject, 
			iT.MoveTo.position, GetCurrentVoyageEndPoint(),
			iT.MoveTo.time, DeployInterval,
			iT.MoveTo.easetype, iTween.EaseType.linear);
		
		yield return new WaitForSeconds (DeployInterval * .9f);
	
		if (Cannon.Instance.CanInteract) {
			m_audioSource.clip = MoveSound;
			m_audioSource.Play ();

			m_canMove = true;
			m_collider.enabled = true;
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
		m_spriteBuilder.Hide ();
		m_lensFlare.enabled = false;
		m_trail.Reset (this);

		Speed *= -1f;
		StartCoroutine (Deploy ());
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		 if (other.IsProjectile ()) {
			m_lensFlare.enabled = false;
			m_trail.enabled = false;
			m_audioSource.Stop ();
			m_audioSource.PlayOneShot (DieSound);
			m_canMove = false;
			Score.Instance.Sum (gameObject, 200);

			Juiceness.Run ("OvniExplosion", () => {
				GetComponentInChildren<SpritePixel3DExplosion> ().Explode ();
			});
		
			StartCoroutine (m_spriteDestruction.DestroySprite ());
		}
	}

	void OnSpriteDestructionEnd() {
		Redeploy ();
	}

	void OnGameOver() {
		m_canMove = false;
		m_audioSource.Stop ();
	}
}
