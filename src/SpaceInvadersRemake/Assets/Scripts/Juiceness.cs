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

	public static YieldInstruction Run(string name, Func<YieldInstruction> juicenessAction, Func<YieldInstruction> notJuicenessAction = null) {
		if (s_juicenessEnabled) {
			Debug.LogFormat ("Running juiceness {0}", name);
			return juicenessAction ();
		} else if (notJuicenessAction == null) {
			return new WaitForFixedUpdate();
		}
		else {
			return notJuicenessAction ();
		}
	}

	public static bool CanRun(string name) {
		return s_juicenessEnabled;
	}
}
