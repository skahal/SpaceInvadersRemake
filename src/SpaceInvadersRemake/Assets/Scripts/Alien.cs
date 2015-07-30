using UnityEngine;
using System.Collections;
using System.Linq;

public class Alien : ShooterBase {
	private AliensWave m_wave;
	private bool m_canShoot;
	private Animator m_animator;

	protected override void Awake ()
	{
		base.Awake ();
		m_animator = GetComponent<Animator> ();
	}

	void Start ()
	{
		m_wave = gameObject.transform.parent.GetComponent<AliensWave> ();
		StartCoroutine (CanShootAgain ());
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.IsVerticalEdge()) {
			m_wave.Flip ();
		}
		else if (collider.IsProjectile()) {
			var projectile = collider.GetComponent<Projectile> ();

			if (projectile.IsTargetingAlien) {
				Game.Instance.AddToScore (10);
				Die ();
			}
		}
	}

	void OnSpawnBegin() {
		m_animator.speed = 0;
	}

	void OnSpawnEnd() {
		m_animator.speed = 1;
	}
		
	protected override bool CanShoot ()
	{
		var result = false;

		if (m_canShoot && Random.Range (0, 1) <= Game.Instance.AlienShootProbability) {
			var hit = Physics2D.LinecastAll (
				transform.position, 
				new Vector2 (transform.position.x, Cannon.Instance.transform.position.y),
				LayerMask.GetMask("Alien"));

			result = hit.Count() == 1;
		} 

		if (m_canShoot) {
			m_canShoot = false;
			StartCoroutine (CanShootAgain ());
		}
	
		return result;
	}

	IEnumerator CanShootAgain() {
		yield return new WaitForSeconds (Random.Range(0, Game.Instance.AlienShootInterval));
		m_canShoot = true;
	}

	void Die() {
		gameObject.SetActive (false);
	}
}
