using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Skahal.Tweening;

[RequireComponent(typeof(Text))]
public class TextSumEffect : MonoBehaviour {

	private Text m_text;
	private int m_currentDisplayValue;
	private int m_value;

	public float Interval = 0.1f;
	public float ColorFactor = 0.001f;
	public string Format = "{0:D}";

	void Awake() {
		m_text = GetComponent<Text> ();
	}

	void Update ()
	{
		Juiceness.Run (
			"TextSumEffect",
			() => {
				m_currentDisplayValue = Mathf.CeilToInt (Mathf.Lerp (m_currentDisplayValue, m_value, Interval));
				m_text.text = string.Format(Format, m_currentDisplayValue);
			},
			() => {
				m_text.text = string.Format(Format, m_value);
			});
	}


	public void Sum(int value) {
		m_value += value;

		Juiceness.Run ("TextSumEffect", () => {
			var currentColor = m_text.color;

			var colorChange = value * ColorFactor;
			currentColor.r += colorChange;
			currentColor.g -= colorChange;
			currentColor.b -= colorChange;

			m_text.color = currentColor;
		});
	}
}
