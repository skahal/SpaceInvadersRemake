using UnityEngine;
using System.Collections;

public class Ovni : MonoBehaviour
{
	private SpriteBuilder m_spriteBuilder;
	private SpriteDestruction m_spriteDestruction;
	private bool m_canMove;
	private BoxCollider2D m_collider;
	private AudioSource m_audioSource;
	private LensFlare m_lensFlare;
	private TrailRenderer m_trail;

	public float DeployInterval = 12f;
	public float Speed = .1f;
	public AudioClip MoveSound;
	public AudioClip DieSound;

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
		}
	}

	IEnumerator Deploy ()
	{	
		m_collider.enabled = false;
		m_spriteBuilder.Hide ();
		m_canMove = false;
		m_audioSource.Stop ();
		m_lensFlare.enabled = false;
		m_trail.enabled = false;
		yield return new WaitForSeconds (DeployInterval);
	
		if (Cannon.Instance.CanInteract) {
			m_audioSource.clip = MoveSound;
			m_audioSource.Play ();

			float x = 0;

			if (Speed > 0) {
				x = Game.Instance.LeftEdge.transform.position.x;
			} else {
				x = Game.Instance.RightEdge.transform.position.x;
			}

			transform.position = new Vector3 (x, transform.position.y, 0);
			m_spriteBuilder.Show ();
			m_canMove = true;
			m_collider.enabled = true;
			m_lensFlare.enabled = true;
			m_trail.enabled = true;
		} else {
			Redeploy ();
		}
	}

	void Redeploy() {
		Speed *= -1f;
		StartCoroutine (Deploy ());
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.IsAlienVerticalEdge ()) {
			Redeploy ();
		} else if (other.IsProjectile ()) {
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
