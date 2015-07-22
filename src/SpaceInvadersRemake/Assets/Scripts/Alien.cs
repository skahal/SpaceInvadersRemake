using UnityEngine;
using System.Collections;
using System.Linq;

public class Alien : ShooterBase {
	private AliensWave m_wave;
	private Cannon m_cannon;
	private bool m_canShoot;

	void Start()
	{
		m_wave = gameObject.transform.parent.GetComponent<AliensWave> ();
		m_cannon = GameObject.FindGameObjectWithTag ("Cannon").GetComponent<Cannon>();

		StartCoroutine (CanShootAgain ());
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.tag == "VerticalEdge") {
			m_wave.Flip ();
		}
		else if (collider.tag == "Projectile") {
			var projectile = collider.GetComponent<Projectile> ();

			if (projectile.TargetTag == "Alien") {
				Game.Instance.AddToScore (10);
				Die ();
			}
		}
	}
		
	protected override bool CanShoot ()
	{
		var result = false;

		if (m_canShoot && Random.Range (0, 1) <= Game.Instance.AlienShootProbability) {
			var hit = Physics2D.LinecastAll (
				          transform.position, 
				          new Vector2 (transform.position.x, m_cannon.transform.position.y));

			var count = hit.Count (h => h.collider.gameObject.layer == gameObject.layer);

			Debug.Log (gameObject.name + ": " + count);
			result = count == 1;
		} 

		if (m_canShoot) {
			m_canShoot = false;
			StartCoroutine (CanShootAgain ());
		}
	
		return result;
	}

	IEnumerator CanShootAgain() {
		yield return new WaitForSeconds (Game.Instance.AlienShootInterval);
		m_canShoot = true;
	}

	void Die() {
		gameObject.SetActive (false);
	}
}
