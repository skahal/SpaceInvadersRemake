using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class Bunker : MonoBehaviour
{
    private SpriteBuilder m_spriteBuilder;
    public int MaxShootSupportedInPoint = 4;
    public int HorizontalPixelsDestroyedPerShoot = 6;
    public int MinimumHitPixelsToBlockProjectile = 20;

    void Awake()
    {
        m_spriteBuilder = GetComponent<SpriteBuilder>().Build();
    }

    bool DestroyPoint(RaycastHit2D hit, bool isAlienTarget)
    {
        var point = hit.point;
        var sprite = m_spriteBuilder.Sprite;
        var bunkerScale = sprite.rect.width;
        var spriteRect = sprite.rect;
        var translatedX = (point.x - transform.position.x) + .5f;
        var projectileX = Mathf.RoundToInt(spriteRect.x + translatedX * bunkerScale);

        var halfWidth = HorizontalPixelsDestroyedPerShoot / 2;
        var projectileWidth = HorizontalPixelsDestroyedPerShoot;
        var bunkerHeight = Mathf.RoundToInt(spriteRect.height);
        var hitPixelsByShoot = ((bunkerHeight / MaxShootSupportedInPoint) * projectileWidth) + projectileWidth;

        var hitPixelsCount = 0;

        if (isAlienTarget)
        {
            for (int pixelY = 0; pixelY < bunkerHeight; pixelY++)
            {
                DestroyHorizontalPizels(pixelY, projectileX, halfWidth, hitPixelsByShoot, ref hitPixelsCount);
            }
        }
        else
        {
            for (int pixelY = bunkerHeight; pixelY > 0; pixelY--)
            {
                DestroyHorizontalPizels(pixelY, projectileX, halfWidth, hitPixelsByShoot, ref hitPixelsCount);
            }
        }

        m_spriteBuilder.Rebuild();

        return hitPixelsCount >= MinimumHitPixelsToBlockProjectile;
    }

    void DestroyHorizontalPizels(int pixelY, int x, int halfWidth, int hitPixelsByShoot, ref int hitPixelsCount)
    {
        for (int pixelX = x - halfWidth; pixelX < x + halfWidth; pixelX++)
        {
            if (hitPixelsCount < hitPixelsByShoot && m_spriteBuilder.HasColor(pixelX, pixelY))
            {
                m_spriteBuilder.ClearColor(pixelX, pixelY);
                hitPixelsCount++;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.IsProjectile())
        {
            var projectile = other.GetComponent<Projectile>();
            var hit = Physics2D.CircleCast(transform.position, 1f, Vector3.zero, 1f, LayerMask.GetMask("Projectile"));

            if (DestroyPoint(hit, projectile.IsTargetingAlien))
            {
                projectile.DestroyIt();
            }
        }
        else if (other.IsAlien())
        {
            SendMessageUpwards("OnAlienReachBunker");
        }
    }
}