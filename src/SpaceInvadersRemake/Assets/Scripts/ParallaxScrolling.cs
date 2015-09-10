using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ParallaxScrolling : MonoBehaviour {

	private SpriteRenderer[] m_layers;
	private Vector3 m_lastPovPosition;

	public static ParallaxScrolling Instance;
	public float LayerAcceleration = 0.03f;
	public Vector2 LayerAccelerationMultiplier = new Vector2(5f, 0);
	public GameObject PointOfView;

	void Awake() {
		Instance = this;
		m_layers = GetComponentsInChildren<SpriteRenderer> ();

		for(int i = 0; i < m_layers.Length; i++) {
			// Change layer sprite sorting order.
			m_layers [i].sortingOrder = i - m_layers.Length;
		}
	}

	void Update () {
		Vector3 povPosition = PointOfView != null ? PointOfView.transform.position * LayerAcceleration : Vector3.zero;

		if (m_lastPovPosition != povPosition) {

			for (int i = 0; i < m_layers.Length; i++) {
				var layer = m_layers [i];
					var newPosition = (povPosition * -1);

				// Layers nearest of point of view move faster.
				var thisLayerMultiplier = i > 0 ? LayerAccelerationMultiplier * i : Vector2.one;

				layer.transform.position = new Vector2(newPosition.x * thisLayerMultiplier.x, newPosition.y * thisLayerMultiplier.y);
			}

			m_lastPovPosition = povPosition;
		}
	}
}
