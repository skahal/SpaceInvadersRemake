using UnityEngine;
using System.Collections;
using Skahal.Tweening;
using UnityEngine.UI;

public class Combo : MonoBehaviour {

	private int m_hitsCount;
	private Text m_text;

	public static Combo Instance;

	public int MinHits = 3;
	public int PointsByHit = 10;
	public int PointsMultiplierByHit = 2;
	public Vector3 ShakeAmount = new Vector3(1, 1, 1);
	public float ShakeTime = 1f;
	public string TextFormat = "{0} hits combo: {1} points";

	void Awake () {
		Instance = this;
		m_text = GetComponent<Text> ();
	}
	
	public void OnProjectileHitAlien(GameObject target)
	{
		m_hitsCount++;

		if (m_hitsCount >= MinHits) {
			var hitsToCount =  m_hitsCount - MinHits + 1;
			var points = (PointsByHit * hitsToCount) * hitsToCount;

			m_text.text = string.Format(TextFormat, m_hitsCount, points);
			m_text.enabled = true;

			iTweenHelper.ShakeRotation (
				gameObject, 
				iT.ShakeRotation.amount, ShakeAmount,
				iT.ShakeRotation.time, ShakeTime,
				iT.ShakeRotation.oncomplete, "OnShakeComplete");
			
			Score.Instance.Sum (target, points, false);
		}
	}

	void OnShakeComplete() {
		m_text.enabled = false;
	}

	public void OnProjectileMissAlien(GameObject target)
	{
		m_hitsCount = 0;
	}
}
