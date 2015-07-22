using UnityEngine;
using System.Collections;

public class Bunker : MonoBehaviour {
	private SpriteRenderer m_renderer;
	private Texture2D m_texture;

	void Awake () {
		m_renderer = GetComponentInChildren<SpriteRenderer> ();
		var originalTexture = m_renderer.sprite.texture;
		m_texture = new Texture2D (originalTexture.width, originalTexture.height, TextureFormat.ARGB32, false);
		m_texture.SetPixels32(originalTexture.GetPixels32());
		RefreshSprite ();
	}

	void RefreshSprite ()
	{
		m_texture.Apply ();
		m_renderer.sprite = Sprite.Create (m_texture, m_renderer.sprite.rect, new Vector2 (0.5f, 0.5f), m_renderer.sprite.pixelsPerUnit);
	}

	void DestroyPoint(RaycastHit2D hit) {
		var point = hit.point;

		var scale = m_renderer.sprite.pixelsPerUnit;	
		var x = Mathf.RoundToInt(point.x * scale + m_renderer.sprite.rect.center.x);
		var y = Mathf.RoundToInt(point.y * scale - m_renderer.sprite.rect.center.y);
		var size = scale * hit.collider.bounds.size;
		var halfWidth = Mathf.RoundToInt (size.x * .5f);
		var halfHeight = Mathf.RoundToInt (size.y * .5f);
	
		for(int i = x - halfWidth; i < x + halfWidth; i++) {
			for(int j = y - halfHeight; j < y + halfHeight; j++) {
				m_texture.SetPixel(i, j, Color.clear);
			}
		}

		RefreshSprite ();
		var collider = m_renderer.gameObject.GetComponent<PolygonCollider2D> ();
		Component.Destroy (collider);
		collider = m_renderer.gameObject.AddComponent<PolygonCollider2D> ();
		//collider.set
	}

	void OnTriggerEnter2D(Collider2D collider) {
		Debug.Log ("OnTriggerEnter2D");
		if (collider.tag == "Projectile") {
			var hit = Physics2D.CircleCast (transform.position, 1, Vector3.zero);
			DestroyPoint (hit);
		}
	}
}