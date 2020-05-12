using UnityEngine;
using Skahal.Threading;
using UnityEngine.SceneManagement;

public class Opening : MonoBehaviour {
	public float DelaytoLoadLevel = 8f;

	void Start() {
		SHCoroutine.Start (DelaytoLoadLevel, () => {
			SceneManager.LoadScene("Main");
		});
	}

	void Update () {
		if (Input.anyKey) {
			SceneManager.LoadScene("Main");
		}
	}
}
