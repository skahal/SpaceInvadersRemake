using UnityEngine;
using System.Collections;
using System.Linq;
using Skahal.Tweening;

/// <summary>
/// Represents an individual alien.
/// </summary>
public class Alien : ShooterBase {
	private AliensWave m_wave;
	private bool m_canShoot;
	private SkeletonAnimation m_animation;
	private AudioSource m_audioSource;

	public int RowScoreFactor = 5;
	public AudioClip DieAudio;
	public Vector3 OtherAlienHitShakeAmount = new Vector3(10f, 10, 10f);

	[HideInInspector] public int Row;
	[HideInInspector] public float AnimationSpeed = 1f;
	[HideInInspector] public bool IsAlive = true;

	protected override void Awake ()
	{
		base.Awake ();
		m_animation = GetComponent<SkeletonAnimation> ();
		m_audioSource = GetComponent<AudioSource> ();
	}

	void Start ()
	{
		
		m_wave = gameObject.transform.parent.GetComponent<AliensWave> ();
		StartCoroutine (CanShootAgain ());
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
				Score.Instance.Sum (gameObject, Row * RowScoreFactor);
				Die ();
			}
		} else if (other.IsCannonZone ()) {
			Cannon.Instance.Die ();
		}
	}

	void OnSpawnBegin() {
		Projectile.DestroyIt ();
	}

	void OnSpawnEnd() {
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

	protected override void PerformShoot ()
	{
		m_animation.state.SetAnimation (0, "Shooting", false).Complete += (Spine.AnimationState state, int trackIndex, int loopCount) => 
		{
			base.PerformShoot ();  
			m_animation.state.SetAnimation (0, "Idle", true);
		};
	}

	void Die() {
		Game.Instance.RaiseMessage ("OnAlienHit", gameObject);
		m_audioSource.PlayOneShot (DieAudio);
		GetComponent<BoxCollider2D> ().enabled = false;

		Juiceness.Run ("AlienExplosion", () => {
			GetComponentInChildren<SpritePixel3DExplosion> ().Explode ();
		});

		CheckAliensAlive ();

		StopAllCoroutines ();
		GetComponent<MeshRenderer> ().enabled = false;
		IsAlive = false;
	}
		
	void CheckAliensAlive() {
		var aliensAlive = m_wave.Aliens.Count (a => a.IsAlive) -1;
	
		if (aliensAlive == 0) {
			Game.Instance.NextLevel ();
		} else {
			SendMessageUpwards ("OnAlienDie", aliensAlive);
		}
	}

	void OnAlienHit(GameObject sender) {
		if (sender != gameObject) {
			Juiceness.Run ("OtherAlienHit", () => {
				iTweenHelper.ShakeRotation (
					gameObject, 
					iT.ShakeRotation.amount, OtherAlienHitShakeAmount);
			});
		}
	}
}
