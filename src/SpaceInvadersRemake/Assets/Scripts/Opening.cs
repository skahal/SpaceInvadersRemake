using UnityEngine;
using System.Collections;
using Skahal.Threading;

public class Opening : MonoBehaviour {
	public float DelaytoLoadLevel = 8f;

	void Start() {
		SHThread.Start (DelaytoLoadLevel, () => {
			Application.LoadLevel ("Main");
		});
	}

	void Update () {
		if (Input.anyKey) {
			Application.LoadLevel ("Main");
		}
	}
}
