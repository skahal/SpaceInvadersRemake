using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {
	[HideInInspector] public static IPlayerInputStrategy Instance;

	void Awake () {
		DisableInput ();
	}

	public static void EnableInput() {
		Instance = new KeyboardPlayerInputStrategy ();
	}

	public static void DisableInput() {
		Instance = new EmptyPlayerInputStrategy ();
	}

	public static void DemoInput() {
		Instance = new DemoPlayerInputStrategy ();
	}
}
