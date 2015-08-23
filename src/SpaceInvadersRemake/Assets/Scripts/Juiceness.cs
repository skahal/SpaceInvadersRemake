using UnityEngine;
using System.Collections;
using System;

public class Juiceness : MonoBehaviour {

	private static bool s_juicenessEnabled = true;

	void Update () {
		if (Input.GetKeyDown (KeyCode.J)) {
			s_juicenessEnabled = !s_juicenessEnabled;
		}
	}

	public static void Run(string name, Action juicenessAction, Action notJuicenessAction = null) {
		if (s_juicenessEnabled) {
			Debug.LogFormat ("Running juiceness {0}", name);
			juicenessAction ();
		} else if (notJuicenessAction != null) {
			notJuicenessAction ();
		}
	}
}
