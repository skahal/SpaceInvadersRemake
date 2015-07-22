using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Game : MonoBehaviour {

	private int m_score;
	public static Game Instance;
	public GameObject Cannon;
	public Vector2 CannonDeployPosition = new Vector2(0, -5);
	public GameObject[] Bunkers;
	public GameObject VerticalEdgePrefab;
	public float VerticalEdgeDistance = .5f;
	public float BottomEdgeDistanceY = 5f;
	public GameObject HorizontalEdgePrefab;
	public Text ScoreText;
	public float AlienShootInterval = -5;
	public float AlienShootProbability = 0.5f;

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

		//m_aliensWave.Setup ();
		SetupEdges ();
		SetupCannon ();

		Debug.Log ("Game setup done");
	}

	void SetupEdges() 
	{
		var left =  m_aliensWave.Left - VerticalEdgeDistance - 1;
		var right = m_aliensWave.Right + VerticalEdgeDistance + 1;

		var leftEdge = Instantiate (VerticalEdgePrefab, new Vector3(left, 1f, 0), Quaternion.identity);
		leftEdge.name = "LeftEdge";

		var rightEdge = Instantiate (VerticalEdgePrefab, new Vector3(right, 1f, 0), Quaternion.identity);
		rightEdge.name = "RightEdge";

		var topEdge = Instantiate (HorizontalEdgePrefab, new Vector3(0f, VerticalEdgeDistance, 0), Quaternion.identity);
		topEdge.name = "TopEdge";

		var bottomEdge = Instantiate (HorizontalEdgePrefab, new Vector3(0f, BottomEdgeDistanceY, 0), Quaternion.identity);
		bottomEdge.name = "BottomEdge";
	}

	void SetupCannon()
	{
		Instantiate (Cannon, CannonDeployPosition, Quaternion.identity);
	}

	public void AddToScore(int value)
	{
		m_score += value;
		ScoreText.text = m_score.ToString ("D4");
	}
}
