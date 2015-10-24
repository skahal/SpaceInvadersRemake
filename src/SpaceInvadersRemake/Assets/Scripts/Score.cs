using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Skahal.Tweening;

public class Score : MonoBehaviour
{
	private TextSumEffect m_sumEffect;

	public static Score Instance;
	public GameObject NewPointScrollingTextPrefab;

	public int HitAlienRowPointsMultiplier = 5;
	public int HitAlienRowArmPointsMultiplier = 1;
	public int HitOvniPoints = 200;

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
			TextHelper.CreateInWorldPoint (
				newPoints.ToString (), 
				source.transform.position,
				NewPointScrollingTextPrefab,
				transform.parent);
		}

		Points += newPoints;

		m_sumEffect.Sum (newPoints);
	}

	public void Sum (GameObject source, bool scrollingText = true)
	{
		int newPoints = GetPointsBySource (source);

		Sum (source, newPoints, scrollingText);
	}

	int GetPointsBySource(GameObject source)
	{
		switch (source.tag) {
		case "Alien":
			var alien = source.GetComponent<Alien> ();
			return alien.Row * HitAlienRowPointsMultiplier;

		case "AlienArm":
			var parentAlien = source.GetComponentInParent<Alien> ();
			return parentAlien.Row * HitAlienRowArmPointsMultiplier;

		case "Ovni":
			return HitOvniPoints;

		default:
			Debug.LogErrorFormat ("'{0}' does not have a score defined", source.tag);
			return 0;
		}
	}
}
