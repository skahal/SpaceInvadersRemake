using UnityEngine;
using System.Collections;

public class Wave : MonoBehaviour {
	
	void Start ()
	{
		GetComponent<TextSumEffect>().Sum (Game.Instance.WaveNumber);
	}
}
