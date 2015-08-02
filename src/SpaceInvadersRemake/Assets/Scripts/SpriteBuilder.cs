using UnityEngine;
using System.Collections;

public class SpriteBuilder {
	private SpriteRenderer m_renderer; 
	private Texture2D m_texture;

	public Sprite Sprite;

	public SpriteBuilder(SpriteRenderer renderer) {
		if (renderer == null) {
			Debug.LogError ("Null renderer for SpriteRenderer");
		}

		m_renderer = renderer;
	}

	public bool HasNoColor(int x, int y) {
		return m_texture.GetPixel (x, y) == Color.clear;
	}

	public bool HasColor(int x, int y) {
		return m_texture.GetPixel (x, y) != Color.clear;
	}

	public SpriteBuilder ClearColor(int x, int y) {
		m_texture.SetPixel (x, y, Color.clear);

		return this;
	}

	public SpriteBuilder Hide() {
		m_renderer.enabled = false;

		return this;
	}

	public SpriteBuilder Show() {
		m_renderer.enabled = true;

		return this;
	}

	public SpriteBuilder Build() {
		Sprite = m_renderer.sprite;

		if (Sprite == null) {
			Debug.LogError ("Null Sprite for SpriteRenderer");
		}

		var originalTexture = Sprite.texture;
		m_texture = new Texture2D (originalTexture.width, originalTexture.height, TextureFormat.ARGB32, false);
		m_texture.SetPixels32 (originalTexture.GetPixels32 ());
		return Rebuild ();	
	}

	public SpriteBuilder Rebuild() {
		m_texture.Apply ();
		m_renderer.sprite = Sprite.Create (m_texture, Sprite.rect, new Vector2 (0.5f, 0.5f), Sprite.pixelsPerUnit);

		return this;
	}
}