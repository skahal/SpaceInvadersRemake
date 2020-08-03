using UnityEngine;
using System;

public class Juiceness : MonoBehaviour {

	private static bool _juicenessEnabled = true;

	void Update () {
		if (Input.GetKeyDown (KeyCode.J)) {
			_juicenessEnabled = !_juicenessEnabled;
		}
	}

	public static void Run(string name, Action juicenessAction, Action notJuicenessAction = null) {
		if (_juicenessEnabled) {
			juicenessAction ();
		} else if (notJuicenessAction != null) {
			notJuicenessAction ();
		}
	}

	public static YieldInstruction Run(string name, Func<YieldInstruction> juicenessAction, Func<YieldInstruction> notJuicenessAction = null) {
		if (_juicenessEnabled) {
			return juicenessAction ();
		} else if (notJuicenessAction == null) {
			return new WaitForFixedUpdate();
		}
		else {
			return notJuicenessAction ();
		}
	}

	public static bool CanRun(string name) {
		return _juicenessEnabled;
	}
}
