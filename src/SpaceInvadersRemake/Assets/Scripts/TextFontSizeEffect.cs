using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Skahal.Tweening;

[RequireComponent(typeof(Text))]
public class TextFontSizeEffect : MonoBehaviour {

	private Text _text;
	private int _originalFontSize;

	public float Interval = 1f;
	public iTween.EaseType Easing = iTween.EaseType.easeOutBack;
	public float From = 0;
	public float To = 1;
	public bool DestroyOnComplete = false;

	void Awake() {
		_text = GetComponent<Text> ();
		_originalFontSize = _text.fontSize;

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
		_text.fontSize = Mathf.FloorToInt(_originalFontSize * value);
	}

	void OnComplete() {
		if (DestroyOnComplete) {
			_text.fontSize = 0;
			Destroy (gameObject);
		}
	}
}
