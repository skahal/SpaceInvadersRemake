using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Skahal.Tweening;

[RequireComponent(typeof(Text))]
public class TextEffect : MonoBehaviour {

	private Text m_text;
	private int m_originalFontSize;

	public float DeployInterval = 1f;
	public iTween.EaseType DeployEasing = iTween.EaseType.easeInBounce;

	void Awake() {
		m_text = GetComponent<Text> ();
		m_originalFontSize = m_text.fontSize;

		Juiceness.Run (
			"ChangeFontSize",
			() => {
				iTweenHelper.ValueTo(
					gameObject,
					iT.ValueTo.from, 0,
					iT.ValueTo.to, 1,
					iT.ValueTo.time, DeployInterval,
					iT.ValueTo.easetype, DeployEasing, 
					iT.ValueTo.onupdate, "ChangeFontSize");
			});
	}

	void ChangeFontSize(float value) {
		m_text.fontSize = Mathf.FloorToInt(m_originalFontSize * value);
	}
}
