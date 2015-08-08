using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Game : MonoBehaviour {

	private int m_score;
	private Bunkers m_bunkers;

	public static Game Instance;
	public GameObject CannonPrefab;
	public Vector2 CannonDeployPosition = new Vector2(0, -5);
	public GameObject VerticalEdgePrefab;
	public float VerticalEdgeDistance = 3;
	public float AlienVerticalEdgeDistance = 5;
	public float TopEdgeDistanceY = 5f;
	public float BottomEdgeDistanceY = 5f;
	public GameObject HorizontalEdgePrefab;
	public Text ScoreText;
	public Text LifesText;
	public float AlienShootInterval = -5;
	public float AlienShootProbability = 0.5f;

	[HideInInspector] public int WaveNumber = 1;
	[HideInInspector] public AliensWave AliensWave;
	[HideInInspector] public GameObject LeftEdge;
	[HideInInspector] public GameObject TopEdge;
	[HideInInspector] public GameObject RightEdge;
	[HideInInspector] public GameObject BottomEdge;

	void Awake () {
		Instance = this;
		Cursor.visible = false;
		AliensWave = GameObject.FindGameObjectWithTag ("AliensWave").GetComponent<AliensWave>();
		m_bunkers = GameObject.FindGameObjectWithTag ("Bunkers").GetComponent<Bunkers>();

		Setup ();
	}

	void Setup()
	{
		Debug.Log ("Begin game setup...");

		m_bunkers.Setup ();
		AliensWave.Setup ();
		SetupEdges ();
		SetupCannon ();

		// Next level stuffs.
		if (PlayerPrefs.HasKey ("Score")) {
			m_score = PlayerPrefs.GetInt ("Score");
			Cannon.Instance.Lifes = PlayerPrefs.GetInt ("Lifes");
			WaveNumber = PlayerPrefs.GetInt ("WaveNumber") + 1;
			PlayerPrefs.DeleteAll ();

			RefreshScore ();
			RefreshLifes ();
		}

		Debug.LogFormat ("Game setup done. Wave number : {0}", WaveNumber);
	}

	void SetupEdges() 
	{
		var left =  AliensWave.Left - VerticalEdgeDistance - 1;
		var right = AliensWave.Right + VerticalEdgeDistance + 1;

		var alienLeft =  AliensWave.Left - AlienVerticalEdgeDistance - 1;
		var alienRight = AliensWave.Right + AlienVerticalEdgeDistance + 1;

		LeftEdge = Instantiate (VerticalEdgePrefab, new Vector3(left, 1f, 0), Quaternion.identity) as GameObject;
		LeftEdge.name = "LeftEdge";

		var alienLeftEdge = Instantiate (VerticalEdgePrefab, new Vector3(alienLeft, 1f, 0), Quaternion.identity) as GameObject;
		alienLeftEdge.name = "AlienLeftEdge";
		alienLeftEdge.tag = "AlienVerticalEdge";
		alienLeftEdge.GetComponentInChildren<SpriteRenderer> ().enabled = false;

		RightEdge = Instantiate (VerticalEdgePrefab, new Vector3(right, 1f, 0), Quaternion.identity) as GameObject;
		RightEdge.name = "RightEdge";
		// Rotate it to invert sprite position.
		RightEdge.transform.Rotate(0, 180, 0);


		var alienRightEdge = Instantiate (VerticalEdgePrefab, new Vector3(alienRight, 1f, 0), Quaternion.identity) as GameObject;
		alienRightEdge.name = "AlienRightEdge";
		alienRightEdge.tag = "AlienVerticalEdge";
		alienRightEdge.GetComponentInChildren<SpriteRenderer> ().enabled = false;

		TopEdge = Instantiate (HorizontalEdgePrefab, new Vector3(0f, TopEdgeDistanceY, 0), Quaternion.identity) as GameObject;
		TopEdge.name = "TopEdge";

		BottomEdge = Instantiate (HorizontalEdgePrefab, new Vector3(0f, BottomEdgeDistanceY, 0), Quaternion.identity) as GameObject;
		BottomEdge.name = "BottomEdge";

		var cannonZone = Instantiate (HorizontalEdgePrefab, CannonDeployPosition, Quaternion.identity) as GameObject;
		cannonZone.tag = "CannonZone";
		cannonZone.name = "CannonZone";
	}

	void SetupCannon()
	{
		Instantiate (CannonPrefab, CannonDeployPosition, Quaternion.identity);
	}

	public void AddToScore(int value)
	{
		m_score += value;
		RefreshScore ();
	}

	void RefreshScore() {
		ScoreText.text = m_score.ToString ("D4");
	}

	void Update() {
		if (Input.GetKeyDown (KeyCode.Return)) {
			Restart ();
		} else if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit ();
		}
	}

	static void Restart ()
	{
		Application.LoadLevel (Application.loadedLevelName);
	}

	public void NextLevel ()
	{
		PlayerPrefs.SetInt ("Score", m_score);
		PlayerPrefs.SetInt ("Lifes", Cannon.Instance.Lifes);
		PlayerPrefs.SetInt ("WaveNumber", WaveNumber);
		Application.LoadLevel (Application.loadedLevelName);
	}

	void OnSpawnBegin() {
		LifesText.enabled = true;
		RefreshLifes ();
	}

	void RefreshLifes() {
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

		gameObject.SendMessage (message, SendMessageOptions.DontRequireReceiver);
	}
}
