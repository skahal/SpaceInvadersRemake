using UnityEngine;

[ExecuteInEditMode]
public class ParallaxScrolling : MonoBehaviour {

	private SpriteRenderer[] _layers;
	private Vector3 _lastPovPosition;

	public static ParallaxScrolling Instance;
	public float LayerAcceleration = 0.03f;
	public Vector2 LayerAccelerationMultiplier = new Vector2(5f, 0);
	public GameObject PointOfView;

	void Awake() {
		Instance = this;
		_layers = GetComponentsInChildren<SpriteRenderer> ();

		for(int i = 0; i < _layers.Length; i++) {
			// Change layer sprite sorting order.
			_layers [i].sortingOrder = i - _layers.Length;
		}
	}

	void Update () {
		Vector3 povPosition = PointOfView != null ? PointOfView.transform.position * LayerAcceleration : Vector3.zero;

		if (_lastPovPosition != povPosition) {

			for (int i = 0; i < _layers.Length; i++) {
				var layer = _layers [i];
					var newPosition = (povPosition * -1);

				// Layers nearest of point of view move faster.
				var thisLayerMultiplier = i > 0 ? LayerAccelerationMultiplier * i : Vector2.one;

				layer.transform.position = new Vector2(newPosition.x * thisLayerMultiplier.x, newPosition.y * thisLayerMultiplier.y);
			}

			_lastPovPosition = povPosition;
		}
	}
}
