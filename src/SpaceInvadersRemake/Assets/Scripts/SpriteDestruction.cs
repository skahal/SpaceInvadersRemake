using System.Collections;
using UnityEngine;

[RequireComponent (typeof(Animator))]
public class SpriteDestruction : MonoBehaviour
{
	private SpriteBuilder m_spriteBuilder;
	private Animator m_animator;

	public float TimeToDestroy = 1;
	public int DestroySquarePixelSize = 10;
	public int DestroySquareCount = 5;

	void Awake ()
	{
		m_animator = GetComponent<Animator> ();
		m_spriteBuilder = GetComponent<SpriteBuilder> ();
	}

	public IEnumerator DestroySprite ()
	{
		m_animator.enabled = false;
		m_spriteBuilder.Build ();
		var rect = m_spriteBuilder.Sprite.rect;
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
					m_spriteBuilder.ClearColor (x, y);
				}
			}

			m_spriteBuilder.Rebuild ();
			yield return new WaitForSeconds (interval);
		}

		m_spriteBuilder.Build ();

		SendMessageUpwards ("OnSpriteDestructionEnd");
	}
}


