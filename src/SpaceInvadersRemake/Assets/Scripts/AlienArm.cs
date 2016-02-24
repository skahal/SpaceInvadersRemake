using UnityEngine;
using System.Collections;
using Skahal.Tweening;

public class AlienArm : MonoBehaviour {

	public SkeletonUtilityBone SkeletonUtilityBone;
	public float ChoppingOffTime = 3;
	public float ChoppingOffToY = 5;
	public Vector3 ChoppingOffRotation = new Vector3 (10, 10, 10);
	public Vector3 ChoppingOffScale = new Vector3 (.5f, .5f, .5f);
	public GameObject BloodSplashPrefab;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.IsProjectile ()) {
			Score.Instance.Sum (gameObject);
			//Instantiate (BloodSplashPrefab, transform.position, Quaternion.identity);
			var explosion = GetComponent<SpritePixel3DExplosion> ();
				
			if(explosion != null) {
				explosion.Explode ();
			}

			ChoppingOff ();
		}
	}

	void ChoppingOff()
	{
		iTweenHelper.RotateBy(
			SkeletonUtilityBone.gameObject,
			iT.RotateBy.time, ChoppingOffTime,
			iT.RotateBy.amount, ChoppingOffRotation);

		iTweenHelper.ScaleTo(
			SkeletonUtilityBone.gameObject,
			iT.ScaleTo.time, ChoppingOffTime,
			iT.ScaleTo.scale, ChoppingOffScale);

		iTweenHelper.MoveTo(
			SkeletonUtilityBone.gameObject,
			iT.MoveTo.time, ChoppingOffTime,
			iT.MoveTo.y, ChoppingOffToY,
			iT.MoveTo.oncompletetarget, gameObject,
			iT.MoveTo.oncomplete, "ChoppingOffOnComplete");
	}

	void ChoppingOffOnComplete() {

	}
}
