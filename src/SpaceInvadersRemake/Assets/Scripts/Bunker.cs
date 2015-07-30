using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class Bunker : MonoBehaviour
{
	private SpriteRenderer m_renderer;
	private Texture2D m_texture;
	public int MaxShootSupportedInPoint = 4;
	public int HorizontalPixelsDestroyedPerShoot = 6;

	void Awake ()
	{
		m_renderer = GetComponentInChildren<SpriteRenderer> ();
			
		var originalTexture = m_renderer.sprite.texture;
		m_texture = new Texture2D (originalTexture.width, originalTexture.height, TextureFormat.ARGB32, false);
		m_texture.SetPixels32 (originalTexture.GetPixels32 ());
		RefreshSprite ();
	}

	void RefreshSprite ()
	{
		m_texture.Apply ();
		m_renderer.sprite = Sprite.Create (m_texture, m_renderer.sprite.rect, new Vector2 (0.5f, 0.5f), m_renderer.sprite.pixelsPerUnit);
	}

	bool DestroyPoint2 (RaycastHit2D hit)
	{
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

	bool DestroyPoint (RaycastHit2D hit, bool isAlienTarget)
	{
		var point = hit.point;
		var sprite = m_renderer.sprite;
		var bunkerScale = sprite.rect.width;
		var spriteRect = sprite.rect;
		var translatedX = (point.x - transform.position.x) + .5f;
		var projectileX = Mathf.RoundToInt (spriteRect.x + translatedX * bunkerScale);
	
		var halfWidth = HorizontalPixelsDestroyedPerShoot / 2;
		var projectileWidth = HorizontalPixelsDestroyedPerShoot;
		var bunkerHeight = Mathf.RoundToInt (spriteRect.height);
		var hitPixelsByShoot = ((bunkerHeight / MaxShootSupportedInPoint) * projectileWidth) + projectileWidth;
	
		var hitPixelsCount = 0;

		if (isAlienTarget) {
			for (int pixelY = 0; pixelY < bunkerHeight; pixelY++) {
				DestroyHorizontalPizels (pixelY, projectileX, halfWidth, hitPixelsByShoot, ref hitPixelsCount);
			}
		} else {
			for (int pixelY = bunkerHeight; pixelY > 0; pixelY--) {
				DestroyHorizontalPizels (pixelY, projectileX, halfWidth, hitPixelsByShoot, ref hitPixelsCount);
			}
		}
			
		RefreshSprite ();

		return hitPixelsCount > 0;	
	}

	void DestroyHorizontalPizels (int pixelY, int x, int halfWidth, int hitPixelsByShoot, ref int hitPixelsCount)
	{
		for (int pixelX = x - halfWidth; pixelX < x + halfWidth; pixelX++) {
			if (hitPixelsCount < hitPixelsByShoot && m_texture.GetPixel (pixelX, pixelY) != Color.clear) {
				m_texture.SetPixel (pixelX, pixelY, Color.clear);
				hitPixelsCount++;
			}
		}
	}

	void OnTriggerEnter2D (Collider2D collider)
	{
		if (collider.IsProjectile()) {
			var projectile = collider.GetComponent<Projectile> ();
			var hit = Physics2D.CircleCast (transform.position, 1f, Vector3.zero, 1f, LayerMask.GetMask ("Projectile"));
		
			if (DestroyPoint (hit, projectile.IsTargetingAlien)) {
				projectile.DestroyIt ();
			}	
		}
	}
}