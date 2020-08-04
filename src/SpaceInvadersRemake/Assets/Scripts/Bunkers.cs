using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Skahal.Tweening;

public class Bunkers : MonoBehaviour {
	public GameObject BunkerPrefab;
	public int BunkersCount = 3;
	public float Width = 100;
	public float DeployInterval = .2f;
	public Vector3 DeployFromScale = Vector3.zero;	
	public iTween.EaseType DeployEasyType = iTween.EaseType.linear;
	public float DeployEasyTime = 1f;

	public void Setup ()
	{
		StartCoroutine(Deploy ());
	}

	IEnumerator Deploy ()
	{
		var distance = Width / BunkersCount;

		for (int i = 0; i < BunkersCount; i++) {
			var bunker = Instantiate (BunkerPrefab, new Vector2 (transform.position.x + i * distance, transform.position.y), Quaternion.identity) as GameObject;
			bunker.transform.parent = transform;

			iTweenHelper.ScaleFrom (
				bunker, 
				iT.ScaleFrom.scale, DeployFromScale,
				iT.ScaleFrom.easetype, DeployEasyType,
				iT.ScaleFrom.time, DeployEasyTime
			);

			yield return new WaitForSeconds(DeployInterval);
			
		}
	}

	public void OnAlienReachBunker() {
		gameObject.SetActive (false);
	}
}
