using UnityEngine;
using System.Collections;

public static class Texture2DExtensions {

	public static Pixel[] ToPixels(this Sprite sprite) {
		var texture = sprite.texture;
		var rect = sprite.rect;
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
				pixels[index++] = new Pixel(x - left, y - top, texture.GetPixel(x, y));
			}
		}

		return pixels;
	}
}
