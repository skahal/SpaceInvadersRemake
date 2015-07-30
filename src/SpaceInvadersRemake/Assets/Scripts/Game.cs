using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Game : MonoBehaviour {

	private int m_score;
	public static Game Instance;
	public GameObject CannonPrefab;
	public Vector2 CannonDeployPosition = new Vector2(0, -5);
	public GameObject VerticalEdgePrefab;
	public float VerticalEdgeDistance = 3;
	public float TopEdgeDistanceY = 5f;
	public float BottomEdgeDistanceY = 5f;
	public GameObject HorizontalEdgePrefab;
	public Text ScoreText;
	public Text LifesText;
	public float AlienShootInterval = -5;
	public float AlienShootProbability = 0.5f;

	[HideInInspector] public AliensWave AliensWave;
	private Bunkers m_bunkers;

	void Awake () {
		Instance = this;

		AliensWave = GameObject.FindGameObjectWithTag ("AliensWave").GetComponent<AliensWave>();
		m_bunkers = GameObject.FindGameObjectWithTag ("Bunkers").GetComponent<Bunkers>();

		Setup ();
	}

	void Setup()
	{
		Debug.Log ("Begin game setup...");

		AliensWave.Setup ();
		SetupEdges ();
		m_bunkers.Setup ();
		SetupCannon ();

		Debug.Log ("Game setup done");
	}

	void SetupEdges() 
	{
		var left =  AliensWave.Left - VerticalEdgeDistance - 1;
		var right = AliensWave.Right + VerticalEdgeDistance + 1;

		var leftEdge = Instantiate (VerticalEdgePrefab, new Vector3(left, 1f, 0), Quaternion.identity);
		leftEdge.name = "LeftEdge";

		var rightEdge = Instantiate (VerticalEdgePrefab, new Vector3(right, 1f, 0), Quaternion.identity);
		rightEdge.name = "RightEdge";

		var topEdge = Instantiate (HorizontalEdgePrefab, new Vector3(0f, TopEdgeDistanceY, 0), Quaternion.identity);
		topEdge.name = "TopEdge";

		var bottomEdge = Instantiate (HorizontalEdgePrefab, new Vector3(0f, BottomEdgeDistanceY, 0), Quaternion.identity);
		bottomEdge.name = "BottomEdge";
	}

	void SetupCannon()
	{
		Instantiate (CannonPrefab, CannonDeployPosition, Quaternion.identity);
	}

	public void AddToScore(int value)
	{
		m_score += value;
		ScoreText.text = m_score.ToString ("D4");
	}

	void Update() {
		if (Input.GetKeyDown (KeyCode.Return)) {
			Application.LoadLevel (Application.loadedLevelName);
		}
	}

	void OnSpawnBegin() {
		LifesText.enabled = true;
		LifesText.text = Cannon.Instance.Lifes.ToString ();
	}

	void OnSpawnEnd() {
		LifesText.enabled = false;
	}

	public void RaiseMessage(string message) {
		Debug.Log ("RaiseMessage: " + message);

		foreach (var alien in AliensWave.Aliens) {
			alien.SendMessage (message, SendMessageOptions.DontRequireReceiver);
		}

		gameObject.SendMessage (message);
	}
}
