using UnityEngine;
using System.Collections;
using Skahal.Tweening;
using UnityEngine.UI;

public class Combo : MonoBehaviour {

	private int _hitsCount;
	private Text _text;

	public static Combo Instance;

	public int MinHits = 3;
	public int PointsByHit = 10;
	public int PointsMultiplierByHit = 2;
	public Vector3 ShakeAmount = new Vector3(1, 1, 1);
	public float ShakeTime = 1f;
	public string TextFormat = "{0} hits combo: {1} points";

	void Awake () {
		Instance = this;
		_text = GetComponent<Text> ();
	}
	
	public void OnProjectileHitAlien(GameObject target)
	{
		_hitsCount++;

		if (_hitsCount >= MinHits) {
			var hitsToCount =  _hitsCount - MinHits + 1;
			var points = (PointsByHit * hitsToCount) * hitsToCount;

			_text.text = string.Format(TextFormat, _hitsCount, points);
			_text.enabled = true;

			iTweenHelper.ShakeRotation (
				gameObject, 
				iT.ShakeRotation.amount, ShakeAmount,
				iT.ShakeRotation.time, ShakeTime,
				iT.ShakeRotation.oncomplete, "OnShakeComplete");
			
			Score.Instance.Sum (target, points, false);
		}
	}

	void OnShakeComplete() {
		_text.enabled = false;
	}

	public void OnProjectileMissAlien(GameObject target)
	{
		_hitsCount = 0;
	}
}
