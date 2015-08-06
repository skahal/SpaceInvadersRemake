using UnityEngine;
using System.Collections;
using System.Linq;

public class Alien : ShooterBase {
	private AliensWave m_wave;
	private bool m_canShoot;
	private Animator m_animator;
	private SpriteDestruction m_spriteDestruction;

	[HideInInspector] public int Row;

	protected override void Awake ()
	{
		base.Awake ();
		m_animator = GetComponent<Animator> ();
	}

	void Start ()
	{
		m_wave = gameObject.transform.parent.GetComponent<AliensWave> ();
		StartCoroutine (CanShootAgain ());
		m_spriteDestruction = GetComponent<SpriteDestruction> ();
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.IsAlienVerticalEdge ()) {
			m_wave.Flip ();
		} else if (collider.IsProjectile ()) {
			var projectile = collider.GetComponent<Projectile> ();

			if (projectile.IsTargetingAlien) {
				Game.Instance.AddToScore (Row * 5);
				Die ();
			}
		} else if (collider.IsCannonZone ()) {
			Cannon.Instance.Die ();
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

		GetComponent<BoxCollider2D> ().enabled = false;
		StartCoroutine (m_spriteDestruction.DestroySprite ());
	}

	void OnSpriteDestructionEnd() {
		CheckAliensAlive ();
		
		gameObject.SetActive (false);
	}

	void CheckAliensAlive() {
		var aliensAlive = m_wave.Aliens.Count (a => a.activeInHierarchy) -1;
	
		if (aliensAlive == 0) {
			Game.Instance.NextLevel ();
		} else {
			SendMessageUpwards ("OnAlienDie", aliensAlive);
		}
	}
}
