using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteBuilder : MonoBehaviour {
	private SpriteRenderer _renderer; 
	private Texture2D _texture;
	private Texture2D _originalTexture;


	[HideInInspector] public Sprite Sprite;

	void Awake() {
		_renderer = GetComponent<SpriteRenderer>();

		if (_renderer == null) {
			_renderer = GetComponentInChildren<SpriteRenderer>();
		}
	}

	public bool HasNoColor(int x, int y) {
		return _texture.GetPixel (x, y) == Color.clear;
	}

	public bool HasColor(int x, int y) {
		return _texture.GetPixel (x, y) != Color.clear;
	}

	public SpriteBuilder ClearColor(int x, int y) {
		_texture.SetPixel (x, y, Color.clear);

		return this;
	}

	public SpriteBuilder Hide() {
		_renderer.enabled = false;

		return this;
	}

	public SpriteBuilder Show() {
		_renderer.enabled = true;

		return this;
	}

	public SpriteBuilder Build() {
		Sprite = _renderer.sprite;

		if (Sprite == null) {
			Debug.LogError ("Null Sprite for SpriteRenderer");
		}

		if (_originalTexture == null) {
			_originalTexture = Sprite.texture;
		}

		_texture = new Texture2D (_originalTexture.width, _originalTexture.height, TextureFormat.ARGB32, false);
		_texture.SetPixels32 (_originalTexture.GetPixels32 ());
		return Rebuild ();	
	}

	public SpriteBuilder Rebuild() {
		_texture.Apply ();
		_renderer.sprite = Sprite.Create (_texture, Sprite.rect, new Vector2 (0.5f, 0.5f), Sprite.pixelsPerUnit);

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
				pixels[index++] = new Pixel(x - left, y - top, _texture.GetPixel (x, y));
			}
		}

		return pixels;
	}
}