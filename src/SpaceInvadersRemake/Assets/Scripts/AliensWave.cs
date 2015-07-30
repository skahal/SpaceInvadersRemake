using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AliensWave : MonoBehaviour {

	private bool m_moving;
	private bool m_isFliping;

	[HideInInspector] public List<GameObject> Aliens = new List<GameObject>();

	public float Columns = 6;
	public float Rows = 6;
	public GameObject[] AlienPrefabs;
	public Vector2 Padding = new Vector2(20, 10);
	public Vector2 MoveSize = new Vector2(1, 1);
	public float MoveDelay = 2f;

	[HideInInspector] public float Left;
	[HideInInspector] public float Right;

	public void Setup () {

		if (AlienPrefabs.Length < Rows) {
			Rows = AlienPrefabs.Length;
		}

		// Keep the wave in center.
		// Last column should not be count.
		var columnsToWidth = (Columns - 1);

		// The wave width is equal columns and padding sum.
		var width = columnsToWidth + columnsToWidth * Padding.x;

		Left = width * -.5f;
		Right = Left + width;
		var top = transform.position.y;

		// Deploy the aliens.
		for (int x = 0; x < Columns; x++) {
			var xInc = Left + x * Padding.x;

			for(int y = 0; y < Rows; y++) {
				var inc = new Vector2(xInc, top + y * Padding.y);
				var alien = Instantiate(AlienPrefabs[y], new Vector2(x, y) + inc, Quaternion.identity) as GameObject;
				Aliens.Add (alien);
				alien.transform.parent = gameObject.transform;
			}
		}
	}

	void Update() {
		if (Cannon.Instance.CanInteract) {
			StartCoroutine (MoveAliens ());
		}
	}

	IEnumerator MoveAliens() {
		if (!m_moving) {
			m_moving = true;
			foreach (var alien in Aliens) {
				alien.transform.position += new Vector3 (MoveSize.x, 0);
			}

			yield return new WaitForSeconds (MoveDelay);
			m_moving = false;
		}

		yield return null;
	}

	public void Flip() {
		if (!m_isFliping) {
			m_isFliping = true;
			MoveSize.x *= -1;
		}

		StartCoroutine (EndFlip ());
	}

	IEnumerator EndFlip() {
		yield return new WaitForSeconds (MoveDelay * 2);
		m_isFliping = false;
	}
}
