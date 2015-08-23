using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bunkers : MonoBehaviour {
	public GameObject BunkerPrefab;
	public int BunkersCount = 3;
	public float Width = 100;
	public float DeployInterval = .2f;

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
			yield return new WaitForSeconds(DeployInterval);
		}
	}

	public void OnAlienReachBunker() {
		gameObject.SetActive (false);
	}
}
