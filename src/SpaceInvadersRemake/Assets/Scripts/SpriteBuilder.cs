using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteBuilder : MonoBehaviour {
	private SpriteRenderer m_renderer; 
	private Texture2D m_texture;
	private Texture2D m_originalTexture;


	[HideInInspector] public Sprite Sprite;

	void Awake() {
		m_renderer = GetComponent<SpriteRenderer>();

		if (m_renderer == null) {
			m_renderer = GetComponentInChildren<SpriteRenderer>();
		}
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

		if (m_originalTexture == null) {
			m_originalTexture = Sprite.texture;
		}

		m_texture = new Texture2D (m_originalTexture.width, m_originalTexture.height, TextureFormat.ARGB32, false);
		m_texture.SetPixels32 (m_originalTexture.GetPixels32 ());
		return Rebuild ();	
	}

	public SpriteBuilder Rebuild() {
		m_texture.Apply ();
		m_renderer.sprite = Sprite.Create (m_texture, Sprite.rect, new Vector2 (0.5f, 0.5f), Sprite.pixelsPerUnit);

		return this;
	}

	public Pixel[] ToPixels() {
		var rect = Sprite.rect;
		int width = Mathf.RoundToInt (rect.width);
		int height = Mathf.RoundToInt (rect.height);
		var pixels = new Pixel[width * height];
		int index = 0;
		int left = Mathf.RoundToInt (rect.x);
		int right = Mathf.RoundToInt (rect.xMax);
		int top = Mathf.RoundToInt (rect.y);
		int bottom = Mathf.RoundToInt (rect.yMax);

		for(int x = left; x < right; x++) {
			for(int y = top; y < bottom; y++) {
				pixels[index++] = new Pixel(x - left, y - top, m_texture.GetPixel (x, y));
			}
		}

		return pixels;
	}
}