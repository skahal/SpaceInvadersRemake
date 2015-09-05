using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Skahal.Tweening;

public class Score : MonoBehaviour {

	private int m_currentDisplayPoints;

	public static Score Instance;
	public Text ScoreText;
	public float PointSumEffectInterval = 0.1f;
	public float PointSumColorEfectFactor = 0.01f;

	[HideInInspector]
	public int Points;

	void Awake() {
		Instance = this;
	}

	void Update() {
		Juiceness.Run (
			"ScoreSumEffect",
			() => {
				m_currentDisplayPoints = Mathf.CeilToInt (Mathf.Lerp (m_currentDisplayPoints, Points, PointSumEffectInterval));
				ScoreText.text = m_currentDisplayPoints.ToString ("D4");
			},
			() => {
				ScoreText.text = Points.ToString ("D4");
			});
	}

	public void Initialize(int initialPoints) {
		Points = initialPoints;
	}

	public void Sum(int newPoints)
	{
		Points += newPoints;

		Juiceness.Run ("ScoreCollorEffect", () => {
			var currentColor = ScoreText.color;

			var colorChange = newPoints * PointSumColorEfectFactor;
			currentColor.r += colorChange;
			currentColor.g -= colorChange;
			currentColor.b -= colorChange;

			ScoreText.color = currentColor;
		});
	}
}
