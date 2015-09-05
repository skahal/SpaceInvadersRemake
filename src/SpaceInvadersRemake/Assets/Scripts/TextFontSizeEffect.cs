using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Skahal.Tweening;

[RequireComponent(typeof(Text))]
public class TextFontSizeEffect : MonoBehaviour {

	private Text m_text;
	private int m_originalFontSize;

	public float Interval = 1f;
	public iTween.EaseType Easing = iTween.EaseType.easeOutBack;
	public float From = 0;
	public float To = 1;
	public bool DestroyOnComplete = false;

	void Awake() {
		m_text = GetComponent<Text> ();
		m_originalFontSize = m_text.fontSize;

		Juiceness.Run (
			"ChangeFontSizeEffect",
			() => {
				iTweenHelper.ValueTo(
					gameObject,
					iT.ValueTo.from, From,
					iT.ValueTo.to, To,
					iT.ValueTo.time, Interval,
					iT.ValueTo.easetype, Easing, 
					iT.ValueTo.looptype, iTween.LoopType.none,
					iT.ValueTo.onupdate, "OnUpdate",
					iT.ValueTo.oncomplete, "OnComplete"
				);
			});
	}

	void OnUpdate(float value) {
		m_text.fontSize = Mathf.FloorToInt(m_originalFontSize * value);
	}

	void OnComplete() {
		if (DestroyOnComplete) {
			m_text.fontSize = 0;
			Destroy (gameObject);
		}
	}
}
