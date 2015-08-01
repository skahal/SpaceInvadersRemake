using UnityEngine;
using System.Collections;

public static class SpriteExtensions {
	public static void RefreshSprite(this SpriteRenderer renderer, Texture2D texture) {
		var sprite = renderer.sprite;
		texture.Apply ();
		renderer.sprite = Sprite.Create (texture, sprite.rect, new Vector2 (0.5f, 0.5f), sprite.pixelsPerUnit);
	}
}