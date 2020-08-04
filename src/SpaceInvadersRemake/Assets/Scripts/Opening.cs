using UnityEngine;
using Skahal.Threading;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Opening : MonoBehaviour {
	public float DelaytoLoadLevel = 8f;

	void Start() {
		SHCoroutine.Start (DelaytoLoadLevel, () => {
			SceneManager.LoadScene("Main");
		});
	}

	void Update () {
		if (Keyboard.current.anyKey.wasPressedThisFrame) {
			SceneManager.LoadScene("Main");
		}
	}
}
