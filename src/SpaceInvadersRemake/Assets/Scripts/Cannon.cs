using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class Cannon: ShooterBase {

	private bool m_touchingEdge;
	private SpriteRenderer m_renderer;
	public static Cannon Instance;
	public float Speed = .1f;
	public float SpawnTime = 1f;
	public float SpawnFlashTime = .15f;
	public int Lifes = 3;

	[HideInInspector] public bool CanInteract;

	protected override void Awake () {
		Instance = this;
	
		base.Awake ();
		m_renderer = GetComponentInChildren<SpriteRenderer> ();

		StartCoroutine (Spawn ());
	}

	private IEnumerator Spawn () {
		CanInteract = false;
		Game.Instance.RaiseMessage ("OnSpawnBegin");

		transform.position = new Vector2 (0, transform.position.y);

		var flashes = SpawnTime / SpawnFlashTime;

		for (int i = 0; i < flashes; i++) {
			m_renderer.enabled = !m_renderer.enabled;
			yield return new WaitForSeconds (SpawnFlashTime);
		}

		m_renderer.enabled = true;

		if (Lifes > 0) {
			CanInteract = true;
			Game.Instance.RaiseMessage ("OnSpawnEnd");
		} 
	}

	void FixedUpdate ()
	{
		Move ();
	}

	void Move ()
	{
		if (CanInteract) {
			var direction = (float)(Input.GetAxisRaw ("Horizontal"));
			var x = transform.position.x;

			// If is touching edge and is trying to move to edge direction again, 
			// abort the movement.
			if (m_touchingEdge && direction > 0 == x > 0)
				return;

			direction *= Speed;
			transform.position = new Vector3 (x + direction, transform.position.y);
		}
	}

	protected override bool CanShoot ()
	{
		return CanInteract && Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown (KeyCode.X);
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.IsVerticalEdge()) {
			m_touchingEdge = true;
		}
		else if (collider.IsProjectile()) {
			var projectile = collider.GetComponent<Projectile> ();

			if (projectile.IsTargetingCannon) {
				LoseLife ();
			}
		}
		else if (collider.IsAlien ()) {
			Die ();
		}
	}

	void OnTriggerExit2D(Collider2D collider) {
		if (collider.IsVerticalEdge()) {
			m_touchingEdge = false;
		}
	}

	void LoseLife(int lifesLost = 1) {
		Lifes -= lifesLost;
		StartCoroutine (Spawn ());

		if (Lifes == 0) {
			Camera.main.GetComponent<ColorCorrectionCurves> ().enabled = true;
		}
	}
	public void Die() {
		LoseLife (Lifes);
	}
}
