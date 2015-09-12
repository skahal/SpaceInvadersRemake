using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Vexe.Runtime.Types;
using Skahal.Tweening;

/// <summary>
/// Represents an alien wave.
/// <remarks>>
/// In the classic formation is a grid with 6 columns x 6 rows of aliens.
/// </remarks>
/// </summary>
public class AliensWave : BetterBehaviour {

	private bool m_moving;
	private bool m_isFliping;
	private float m_currentMoveDelay = 1.5f;
	private int m_totalAliens;
	private AudioSource m_audioSource;
	private int m_currentAlienMoveSoundIndex;
	private bool m_aliensDeployed = false;

	public float Columns = 6;
	public float Rows = 6;
	public GameObject[] AlienPrefabs;
	public Vector2 Padding = new Vector2(20, 10);
	public Vector2 MoveSize = new Vector2(1, 1);
	public Vector2 MoveSizeWaveNumberInc = new Vector2(0.1f, 0.1f);
	public Dictionary<int, float> AliensAliveMoveDelay;
	public AudioClip[] AlienMoveSounds;
	public AudioClip AlienSpeedChangedSound;
	public float AlienDeployInterval = .005f;
	public Vector3 AlienDeployStartPosition = new Vector3 (0, 5, 0);
	public iTween.EaseType AlienDeployEasyType = iTween.EaseType.linear;
	public float AlienDeployEasyTime = .1f;

	[HideInInspector] public List<Alien> Aliens = new List<Alien>();
	[HideInInspector] public float Left;
	[HideInInspector] public float Right;

	void Awake() {
		m_audioSource = GetComponent<AudioSource> ();

		if (AlienMoveSounds.Length == 0) {
			Debug.LogError ("At least one AlienMoveSounds must be defined.");
		}
	}

	public void Setup () {

		if (AlienPrefabs.Length < Rows) {
			Rows = AlienPrefabs.Length;
		}

		var waveNumber = Game.Instance.WaveNumber;

		if (waveNumber > 1) {
			MoveSize = MoveSize + (MoveSizeWaveNumberInc * waveNumber);
		}

		// Keep the wave in center.
		// Last column should not be count.
		var columnsToWidth = (Columns - 1);

		// The wave width is equal columns and padding sum.
		var width = columnsToWidth * Padding.x;

		Left = width * -.5f;
		Right = Left + width;
		var top = transform.position.y;

		// Deploy the aliens.
		StartCoroutine(DeployAliens (top));
	}

	IEnumerator DeployAliens (float top)
	{
		for (int x = 0; x < Columns; x++) {
			var xInc = Left + x * Padding.x;
			for (int y = 0; y < Rows; y++) {
				var inc = new Vector2 (xInc, top + y * Padding.y);
				var alienGO = Instantiate (AlienPrefabs [y], inc, Quaternion.identity) as GameObject;
				alienGO.name = string.Format ("Alien_{0}x{1}", x, y);
				var alien = alienGO.GetComponent<Alien> ();
				alien.Row = y + 1;
				alien.AnimationSpeed = 1;
				alien.transform.parent = gameObject.transform;
				Aliens.Add (alien);

				if (Juiceness.CanRun ("DeployAliensAnimation")) {
					iTweenHelper.MoveFrom (
						alienGO, 
						iT.MoveFrom.position, AlienDeployStartPosition,
						iT.MoveFrom.easetype, AlienDeployEasyType,
						iT.MoveFrom.time, AlienDeployEasyTime
					);
					yield return new WaitForSeconds (AlienDeployInterval);
				}
			}
		}

		m_totalAliens = Aliens.Count;
		SetDelay (m_totalAliens);

		yield return new WaitForEndOfFrame ();

		m_aliensDeployed = true;
	}

	void FixedUpdate() {
		if (Cannon.Instance.CanInteract && m_aliensDeployed) {
			StartCoroutine (MoveAliens ());
		}
	}

	IEnumerator MoveAliens() {
		if (!m_moving) {
			m_moving = true;

			foreach (var alien in Aliens) {
				alien.Move (MoveSize.x, 0);
			}
				
			PlayAlienMoveSound ();

			yield return new WaitForSeconds (m_currentMoveDelay);
			m_moving = false;
		}

		yield return null;
	}

	void PlayAlienMoveSound ()
	{
		m_audioSource.PlayOneShot (AlienMoveSounds[m_currentAlienMoveSoundIndex]);
		m_currentAlienMoveSoundIndex = m_currentAlienMoveSoundIndex + 1 < AlienMoveSounds.Length ? m_currentAlienMoveSoundIndex + 1 : 0;
	}

	public void Flip() {
		if (!m_isFliping) {
			m_isFliping = true;
			MoveSize.x *= -1;
			transform.position = new Vector3 (transform.position.x, transform.position.y - MoveSize.y, 0);

			StartCoroutine (EndFlip ());
		}
	}

	IEnumerator EndFlip() {
		yield return new WaitForSeconds (m_currentMoveDelay * Columns);
		m_isFliping = false;
	}

	void OnAlienDie(int aliensAlive) {
		SetDelay (aliensAlive);
	}

	void SetDelay(int aliensAlive) {
		if(AliensAliveMoveDelay.ContainsKey(aliensAlive)) {
			m_currentMoveDelay = AliensAliveMoveDelay[aliensAlive];
			m_audioSource.PlayOneShot (AlienSpeedChangedSound);
		}

		Debug.LogFormat ("Move delay for {0} aliens alive is {1}", aliensAlive, m_currentMoveDelay);
	}
}
