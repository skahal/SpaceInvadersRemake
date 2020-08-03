using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(SpriteRenderer))]
public class SpriteBuilderFromMeshRenderer : MonoBehaviour {
	private MeshRenderer _renderer; 
	private Texture2D _texture;
	private Texture2D _originalTexture;

	[HideInInspector] public Texture2D Texture;

	void Awake() {
		_renderer = GetComponent<MeshRenderer>();

		if (_renderer == null) {
			_renderer = GetComponentInChildren<MeshRenderer>();
		}
	}

	public bool HasNoColor(int x, int y) {
		return _texture.GetPixel (x, y) == Color.clear;
	}

	public bool HasColor(int x, int y) {
		return _texture.GetPixel (x, y) != Color.clear;
	}

	public SpriteBuilderFromMeshRenderer ClearColor(int x, int y) {
		_texture.SetPixel (x, y, Color.clear);

		return this;
	}

	public SpriteBuilderFromMeshRenderer Hide() {
		_renderer.enabled = false;

		return this;
	}

	public SpriteBuilderFromMeshRenderer Show() {
		_renderer.enabled = true;

		return this;
	}

	public SpriteBuilderFromMeshRenderer Build() {
		Texture = _renderer.material.mainTexture as Texture2D;

		if (Texture == null) {
			Debug.LogError ("Null Texture for MeshRenderer");
		}

		if (_originalTexture == null) {
			_originalTexture = Texture;
		}

		_texture = new Texture2D (_originalTexture.width, _originalTexture.height, TextureFormat.ARGB32, false);
		_texture.SetPixels32 (_originalTexture.GetPixels32 ());
		return Rebuild ();	
	}

	public SpriteBuilderFromMeshRenderer Rebuild() {
		_texture.Apply ();
		//m_renderer.sprite = Sprite.Create (m_texture, Sprite.rect, new Vector2 (0.5f, 0.5f), Sprite.pixelsPerUnit);
		_renderer.material.mainTexture = _texture;
		return this;
	}

	public Pixel[] ToPixels() {
		var rect = _texture;
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
				pixels[index++] = new Pixel(x - left, y - top, _texture.GetPixel (x, y));
			}
		}

		return pixels;
	}
}