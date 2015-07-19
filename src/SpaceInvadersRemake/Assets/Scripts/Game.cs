using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

	public static Game Instance;
	public GameObject Cannon;
	public Vector2 CannonDeployPosition = new Vector2(0, -5);
	public GameObject[] Bunkers;
	public GameObject VerticalEdge;
	public float VerticalEdgeDistance = .5f;

	private AliensWave m_aliensWave;

	void Awake () {
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy (gameObject);
		
		DontDestroyOnLoad (gameObject);

		m_aliensWave = GameObject.FindGameObjectWithTag ("AliensWave").GetComponent<AliensWave>();
		Setup ();
	}

	void Setup()
	{
		Debug.Log ("Begin game setup...");

		m_aliensWave.Setup ();
		SetupEdges ();
		SetupCannon ();

		Debug.Log ("Game setup done");
	}

	void SetupEdges() 
	{
		var left =  m_aliensWave.Left - VerticalEdgeDistance - 1;
		var right = m_aliensWave.Right + VerticalEdgeDistance + 1;

		var leftEdge = Instantiate (VerticalEdge, new Vector3(left, 1f, 0), Quaternion.identity);
		leftEdge.name = "LeftEdge";

		var rightEdge = Instantiate (VerticalEdge, new Vector3(right, 1f, 0), Quaternion.identity);
		rightEdge.name = "RightEdge";
	}

	void SetupCannon()
	{
		Instantiate (Cannon, CannonDeployPosition, Quaternion.identity);
	}
}
