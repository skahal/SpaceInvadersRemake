using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Skahal.Camera;
using UnityEngine.SceneManagement;
using Skahal.SpaceInvadersRemake;

public class Game : MonoBehaviour {
	private Bunkers _bunkers;
	private GameObject _ovni;
	private AudioSource _audioSource;

	public static Game Instance;
	public GameObject CannonPrefab;
	public Vector2 CannonDeployPosition = new Vector2(0, -5);
	public GameObject VerticalEdgePrefab;
	public float VerticalEdgeDistance = 3;
	public float AlienVerticalEdgeDistance = 5;
	public float TopEdgeDistanceY = 5f;
	public float BottomEdgeDistanceY = 5f;
	public GameObject HorizontalEdgePrefab;
	public Text LifesText;
	public float AlienShootInterval = -5;
	public float AlienShootProbability = 0.5f;
	public AudioClip GameOverSound;
	public float EnableInputDelay = 1f;

	[HideInInspector] public int WaveNumber = 1;
	[HideInInspector] public AliensWave AliensWave;
	[HideInInspector] public GameObject LeftEdge;
	[HideInInspector] public GameObject TopEdge;
	[HideInInspector] public GameObject RightEdge;
	[HideInInspector] public GameObject BottomEdge;
	[HideInInspector] public float LeftBorder;
	[HideInInspector] public float RightBorder;
	[HideInInspector] public bool IsGameOver;

	Controls _controls;

	void Awake () {
		LeftBorder = SHCameraHelper.GetLeftBorder ();
		RightBorder = SHCameraHelper.GetRightBorder ();

		Instance = this;
		Cursor.visible = false;
		AliensWave = GameObject.FindGameObjectWithTag ("AliensWave").GetComponent<AliensWave>();
		_bunkers = GameObject.FindGameObjectWithTag ("Bunkers").GetComponent<Bunkers>();
		_ovni = GameObject.Find ("Ovni");
		_audioSource = GetComponent<AudioSource> ();

		Setup ();
	}

	void Setup()
	{
		Debug.Log ("Begin game setup...");
		//PlayerInput.DisableInput ();
	
		_bunkers.Setup ();
		AliensWave.Setup ();
		SetupEdges ();
		SetupCannon ();

		// Next level stuffs.
		if (PlayerPrefs.HasKey ("Score")) {
			Score.Instance.Initialize(PlayerPrefs.GetInt ("Score"));
			Cannon.Instance.Lifes = PlayerPrefs.GetInt ("Lifes");
			WaveNumber = PlayerPrefs.GetInt ("WaveNumber") + 1;
			PlayerPrefs.DeleteAll ();

			RefreshLifes ();
		}

		StartCoroutine (EnableInput ());

		Debug.LogFormat ("Game setup done. Wave number : {0}", WaveNumber);
	}

	IEnumerator EnableInput() {
		Cannon.Instance.CanInteract = false;
		yield return new WaitForSeconds (EnableInputDelay);

		_controls = new Controls();
		_controls.Global.Restart.performed += ctx => Restart();
		_controls.Global.Quit.performed += ctx => Quit();
		_controls.Enable();		
		Cannon.Instance.CanInteract = true;
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
		var cannon = Instantiate (CannonPrefab, CannonDeployPosition, Quaternion.identity) as GameObject;
		ParallaxScrolling.Instance.PointOfView = cannon;
	}	

	public void Restart ()
	{
		//PlayerInput.DisableInput ();
		SceneManager.LoadScene("Main");
	}

	public void Quit()
    {
		Application.Quit();
	}

	public void NextLevel ()
	{
		//PlayerInput.DisableInput ();
		PlayerPrefs.SetInt ("Score", Score.Instance.Points);
		PlayerPrefs.SetInt ("Lifes", Cannon.Instance.Lifes);
		PlayerPrefs.SetInt ("WaveNumber", WaveNumber);
		SceneManager.LoadScene("Main");
	}

	void OnSpawnBegin() {
		//PlayerInput.DisableInput ();
		LifesText.enabled = true;
		RefreshLifes ();
	}

	void RefreshLifes() {
		LifesText.text = Cannon.Instance.Lifes.ToString ();
	}

	void OnSpawnEnd() {
		LifesText.enabled = false;
		//PlayerInput.EnableInput ();
	}

	public void RaiseMessage(string message, GameObject sender = null) {
		//Debug.Log ("RaiseMessage: " + message);

		foreach (var alien in AliensWave.Aliens) {
			alien.SendMessage (message, sender, SendMessageOptions.DontRequireReceiver);
		}
			
		_ovni.SendMessage (message, sender, SendMessageOptions.DontRequireReceiver);
		Combo.Instance.SendMessage (message, sender, SendMessageOptions.DontRequireReceiver);
		gameObject.SendMessage (message, sender, SendMessageOptions.DontRequireReceiver);
	}

	public void StartGameOver() {
		IsGameOver = true;
		_audioSource.clip = GameOverSound;
		_audioSource.Play ();

		//PlayerInput.EnableInput ();
		//TODO: Update this with https://github.com/Unity-Technologies/PostProcessing/releases
		// Camera.main.GetComponent<ColorCorrectionCurves> ().enabled = true;
		RaiseMessage("OnGameOver");
	}
}
