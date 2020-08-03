using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Text helper.
/// </summary>
public static class TextHelper
{
	/// <summary>
	/// Creates the text using the prefab in the world point.
	/// </summary>
	/// <param name="text">Text.</param>
	/// <param name="worldPoint">World point.</param>
	/// <param name="textPrefab">Text prefab.</param>
	/// <param name="canvasTransform">Canvas transform.</param>
	public static Text CreateInWorldPoint(string text, Vector3 worldPoint, GameObject textPrefab, Transform canvasTransform)
	{
		Vector2 viewportPoint = Camera.main.WorldToViewportPoint(worldPoint);

		var textComponent = (GameObject.Instantiate (textPrefab) as GameObject).GetComponent<Text>();
		textComponent.text = text;
		textComponent.transform.SetParent (canvasTransform, false);
		textComponent.transform.localPosition = Vector3.zero;
		var rectTransform = textComponent.GetComponent<RectTransform> ();

		rectTransform.anchorMin = viewportPoint;  
		rectTransform.anchorMax = viewportPoint; 

		return textComponent;
	}
}