using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(SpriteRenderer))]
public class SpriteBuilderFromMeshRenderer : MonoBehaviour {
	private MeshRenderer m_renderer; 
	private Texture2D m_texture;
	private Texture2D m_originalTexture;

	[HideInInspector] public Texture2D Texture;

	void Awake() {
		m_renderer = GetComponent<MeshRenderer>();

		if (m_renderer == null) {
			m_renderer = GetComponentInChildren<MeshRenderer>();
		}
	}

	public bool HasNoColor(int x, int y) {
		return m_texture.GetPixel (x, y) == Color.clear;
	}

	public bool HasColor(int x, int y) {
		return m_texture.GetPixel (x, y) != Color.clear;
	}

	public SpriteBuilderFromMeshRenderer ClearColor(int x, int y) {
		m_texture.SetPixel (x, y, Color.clear);

		return this;
	}

	public SpriteBuilderFromMeshRenderer Hide() {
		m_renderer.enabled = false;

		return this;
	}

	public SpriteBuilderFromMeshRenderer Show() {
		m_renderer.enabled = true;

		return this;
	}

	public SpriteBuilderFromMeshRenderer Build() {
		Texture = m_renderer.material.mainTexture as Texture2D;

		if (Texture == null) {
			Debug.LogError ("Null Texture for MeshRenderer");
		}

		if (m_originalTexture == null) {
			m_originalTexture = Texture;
		}

		m_texture = new Texture2D (m_originalTexture.width, m_originalTexture.height, TextureFormat.ARGB32, false);
		m_texture.SetPixels32 (m_originalTexture.GetPixels32 ());
		return Rebuild ();	
	}

	public SpriteBuilderFromMeshRenderer Rebuild() {
		m_texture.Apply ();
		//m_renderer.sprite = Sprite.Create (m_texture, Sprite.rect, new Vector2 (0.5f, 0.5f), Sprite.pixelsPerUnit);
		m_renderer.material.mainTexture = m_texture;
		return this;
	}

	public Pixel[] ToPixels() {
		var rect = m_texture;
		int width = rect.width;
		int height = rect.height;
		var pixels = new Pixel[width * height];
		int index = 0;
		int left = 0;
		int right = rect.width;
		int top = 0;
		int bottom = rect.height;
	
		for(int x = left; x < right; x++) {
			for(int y = top; y < bottom; y++) {
				pixels[index++] = new Pixel(x - left, y - top, m_texture.GetPixel (x, y));
			}
		}

		return pixels;
	}
}