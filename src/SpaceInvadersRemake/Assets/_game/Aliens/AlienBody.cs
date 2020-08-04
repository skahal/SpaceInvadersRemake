using UnityEngine;

namespace Skahal.SpaceInvadersRemake
{
	public class AlienBody : MonoBehaviour
	{
		private Alien _alien;

		void Start()
		{
			_alien = transform.parent.GetComponent<Alien>();
		}

		void OnTriggerEnter2D(Collider2D other)
		{
			if (other.IsProjectile())
			{
				var projectile = other.GetComponent<Projectile>();

				if (projectile.IsTargetingAlien)
				{
					Score.Instance.Sum(_alien.gameObject);
					_alien.Die();
				}
			}
			else if (other.IsCannonZone())
			{
				Cannon.Instance.Die();
			}
		}
	}
}