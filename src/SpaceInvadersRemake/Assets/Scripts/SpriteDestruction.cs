using System.Collections;
using UnityEngine;

[RequireComponent (typeof(Animator))]
public class SpriteDestruction : MonoBehaviour
{
	private SpriteBuilder _spriteBuilder;
	private Animator _animator;

	public float TimeToDestroy = 1;
	public int DestroySquarePixelSize = 10;
	public int DestroySquareCount = 5;

	void Awake ()
	{
		_animator = GetComponent<Animator> ();
		_spriteBuilder = GetComponent<SpriteBuilder> ();
	}

	public IEnumerator DestroySprite ()
	{
		_animator.enabled = false;
		_spriteBuilder.Build ();
		var rect = _spriteBuilder.Sprite.rect;
		var minX = (int)rect.x;
		var maxX = (int)rect.xMax + 1 - DestroySquarePixelSize;
		var minY = (int)rect.y;
		var maxY = (int)rect.yMax + 1 - DestroySquarePixelSize;
		var interval = TimeToDestroy / DestroySquareCount;

		// Destroy any squares on Alien.
		for (int i = 0; i < DestroySquareCount; i++) {
			// Get random points to square.
			var beginX = Random.Range (minX, maxX);
			var endX = beginX + DestroySquarePixelSize;
			var beginY = Random.Range (minY, maxY);
			var endy = beginY + DestroySquarePixelSize;

			// Draw the square.
			for (var x = beginX; x <= endX; x++) {
				for (var y = beginY; y < endy; y++) {
					_spriteBuilder.ClearColor (x, y);
				}
			}

			_spriteBuilder.Rebuild ();
			yield return new WaitForSeconds (interval);
		}

		_spriteBuilder.Build ();

		SendMessageUpwards ("OnSpriteDestructionEnd");
	}
}


