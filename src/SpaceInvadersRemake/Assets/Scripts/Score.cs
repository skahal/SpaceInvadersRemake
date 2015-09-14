using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Skahal.Tweening;

public class Score : MonoBehaviour
{
	private TextSumEffect m_sumEffect;

	public static Score Instance;
	public GameObject NewPointScrollingTextPrefab;

	[HideInInspector]
	public int Points;

	void Awake ()
	{
		Instance = this;
		m_sumEffect = GetComponent<TextSumEffect> ();
	}
		
	public void Initialize (int initialPoints)
	{
		Points = initialPoints;
		m_sumEffect.Sum (Points);
	}

	public void Sum (GameObject source, int newPoints, bool scrollingText = true)
	{
		if (scrollingText) {
			Juiceness.Run ("ScoreNewPointsEffect", () => {
				TextHelper.CreateInWorldPoint (
					newPoints.ToString (), 
					source.transform.position,
					NewPointScrollingTextPrefab,
					transform.parent);
			});
		}
		
		Points += newPoints;

		m_sumEffect.Sum (newPoints);
	}
}
