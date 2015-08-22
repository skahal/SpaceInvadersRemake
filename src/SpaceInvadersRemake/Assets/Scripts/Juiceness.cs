using UnityEngine;
using System.Collections;
using System;

public class Juiceness : MonoBehaviour {

	private static bool s_juicenessEnabled;

	void Update () {
		if (Input.GetKeyDown (KeyCode.J)) {
			s_juicenessEnabled = !s_juicenessEnabled;
		}
	}

	public static void Run(string name, Action action) {
		if (s_juicenessEnabled) {
			Debug.LogFormat ("Running juiceness {0}", name);
			action ();
		}
	}
}
