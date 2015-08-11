﻿using UnityEngine;
using System.Collections;
using System.Linq;

public class Alien : ShooterBase {
	private AliensWave m_wave;
	private bool m_canShoot;
	private Animator m_animator;
	private SpriteDestruction m_spriteDestruction;
	private AudioSource m_audioSource;

	public int RowScoreFactor = 5;
	public AudioClip DieAudio;
	[HideInInspector] public int Row;
	[HideInInspector] public float AnimationSpeed = 1f;

	protected override void Awake ()
	{
		base.Awake ();
		m_animator = GetComponent<Animator> ();
		m_audioSource = GetComponent<AudioSource> ();
	}

	void Start ()
	{
		m_wave = gameObject.transform.parent.GetComponent<AliensWave> ();
		StartCoroutine (CanShootAgain ());
		m_spriteDestruction = GetComponent<SpriteDestruction> ();
	}

	public void Move(float x, float y) 
	{
		transform.position += new Vector3 (x, y);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.IsAlienVerticalEdge ()) {
			m_wave.Flip ();
		} else if (other.IsProjectile ()) {

			var projectile = other.GetComponent<Projectile> ();

			if (projectile.IsTargetingAlien) {
				Game.Instance.AddToScore (Row * RowScoreFactor);
				Die ();
			}
		} else if (other.IsCannonZone ()) {
			Cannon.Instance.Die ();
		}
	}

	void OnSpawnBegin() {
		Projectile.DestroyIt ();
		m_animator.speed = 0;
	}

	void OnSpawnEnd() {
		m_animator.speed = AnimationSpeed;
	}
		
	protected override bool CanShoot ()
	{
		var result = false;

		if (m_canShoot && Random.Range (0, 1) <= Game.Instance.AlienShootProbability) {
			var hit = Physics2D.LinecastAll (
				transform.position, 
				new Vector2 (transform.position.x, Cannon.Instance.transform.position.y),
				LayerMask.GetMask("Alien"));

			var aliensDownCount = hit.Count ();
		
			result = aliensDownCount == 0;
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
		m_audioSource.PlayOneShot (DieAudio);
		GetComponent<BoxCollider2D> ().enabled = false;
		StartCoroutine (m_spriteDestruction.DestroySprite ());
	}

	void OnSpriteDestructionEnd() {
		CheckAliensAlive ();
		
		gameObject.SetActive (false);
	}

	void CheckAliensAlive() {
		var aliensAlive = m_wave.Aliens.Count (a => a.gameObject.activeInHierarchy) -1;
	
		if (aliensAlive == 0) {
			Game.Instance.NextLevel ();
		} else {
			SendMessageUpwards ("OnAlienDie", aliensAlive);
		}
	}
}
