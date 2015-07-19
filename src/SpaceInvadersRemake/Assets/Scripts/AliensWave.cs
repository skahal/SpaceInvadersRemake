using UnityEngine;
using System.Collections;

public class AliensWave : MonoBehaviour {

	public int Columns = 6;
	public int Rows = 6;
	public GameObject[] Aliens;
	public Vector2 Padding = new Vector2(20, 10);

	public void Setup () {

		if (Aliens.Length < Rows) {
			Rows = Aliens.Length;
		}

		var startX = (Columns * Padding.x) / -2;

		for (int x = 0; x < Columns; x++) {
			var xInc = startX + x * Padding.x;

			for(int y = 0; y < Rows; y++) {
				var inc = new Vector2(xInc, y * Padding.y);
				Instantiate(Aliens[y], new Vector2(x, y) + inc, Quaternion.identity);
			}
		}
	}
}
