using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

	private AliensWave m_aliensWave;

	void Awake () {
		m_aliensWave = GetComponent<AliensWave> ();	
		Setup ();
	}

	void Setup()
	{
		Debug.Log ("Begin game setup...");
	
		m_aliensWave.Setup ();
		
		Debug.Log ("Game setup done");
	}
}
