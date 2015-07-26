using UnityEngine;
using System.Collections;

public class Bunkers : MonoBehaviour {

	public GameObject BunkerPrefab;
	public int BunkersCount = 3;
	public float Width = 100;

	public void Setup ()
	{
		var distance = Width / BunkersCount;

		for (int i = 0; i < BunkersCount; i++) {
			Instantiate (BunkerPrefab, new Vector2 (transform.position.x + i * distance, transform.position.y), Quaternion.identity);
		}
	}
}
