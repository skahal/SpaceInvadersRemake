using UnityEngine;
using System.Collections;

public class Projectile: MonoBehaviour {
	private Vector2? m_target;
	public float Speed = .1f;

	public void SetTarget(Vector2 target) {
		m_target = target;
	}

	void Update () {
		if (m_target.HasValue) {
			transform.position = Vector2.Lerp (transform.position, m_target.Value, Time.deltaTime * Speed);
		}
	}
}
