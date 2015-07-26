using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class Bunker : MonoBehaviour {
	private SpriteRenderer m_renderer;
	private Texture2D m_texture;
	public int MaxShootSupportedInPoint = 4;

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

	bool DestroyPoint2(RaycastHit2D hit) {
		var point = hit.point;
		var sprite = m_renderer.sprite;
		var scale = sprite.rect.width;
		var spriteRect = sprite.rect;
		var translatedX = (point.x - transform.position.x) + .5f;
		var xTexture = spriteRect.x + translatedX * scale;


		Debug.Log (xTexture);

		RefreshSprite ();

		return true;
	}

	bool DestroyPoint(RaycastHit2D hit) {
		var point = hit.point;
		var sprite = m_renderer.sprite;
		var bunkerScale = sprite.rect.width;
		var spriteRect = sprite.rect;
		var translatedX = (point.x - transform.position.x) + .5f;
		var x = Mathf.RoundToInt(spriteRect.x + translatedX * bunkerScale);

		var projectileRenderer = hit.collider.GetComponentInChildren<SpriteRenderer> ();
		var projectileScale = projectileRenderer.sprite.rect.width;
	
		const int halfWidth = 3;
		const int projectileWidth = halfWidth * 2;
		var bunkerHeight = Mathf.RoundToInt (spriteRect.height);
		var hitPixelsByShoot = ((bunkerHeight / MaxShootSupportedInPoint) * projectileWidth) + projectileWidth;
	
		var hitPixelsCount = 0;
		var translatedY = (point.y - transform.position.y) + .5f;
	
		for(int j = 0; j < bunkerHeight; j++) {
			for(int i = x - halfWidth; i < x + halfWidth; i++) {
				if (hitPixelsCount < hitPixelsByShoot && m_texture.GetPixel (i, j) != Color.clear) {
					m_texture.SetPixel (i, j, Color.clear);
					hitPixelsCount++;
				}
			}
		}
			
		RefreshSprite ();
	
		return hitPixelsCount > 0;	
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.tag == "Projectile") {
			var projectile = collider.GetComponent<Projectile> ();

			if (projectile.TargetTag == "Alien") {
				var hit = Physics2D.CircleCast (transform.position, 1, Vector3.zero);
				if (DestroyPoint (hit)) {
					projectile.DestroyIt ();
				}
			}
		}
	}
}