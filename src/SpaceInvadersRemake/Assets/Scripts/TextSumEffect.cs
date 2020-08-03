using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextSumEffect : MonoBehaviour {

	private Text _text;
	private int _currentDisplayValue;
	private int _value;

	public float Interval = 0.1f;
	public float ColorFactor = 0.001f;
	public string Format = "{0:D}";

	void Awake() {
		_text = GetComponent<Text> ();
	}

	void Update ()
	{
		Juiceness.Run (
			"TextSumEffect",
			() => {
				_currentDisplayValue = Mathf.CeilToInt (Mathf.Lerp (_currentDisplayValue, _value, Interval));
				_text.text = string.Format(Format, _currentDisplayValue);
			},
			() => {
				_text.text = string.Format(Format, _value);
			});
	}


	public void Sum(int value) {
		_value += value;

		Juiceness.Run ("TextSumEffect", () => {
			var currentColor = _text.color;

			var colorChange = value * ColorFactor;
			currentColor.r += colorChange;
			currentColor.g -= colorChange;
			currentColor.b -= colorChange;

			_text.color = currentColor;
		});
	}
}
