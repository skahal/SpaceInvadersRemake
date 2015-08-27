using UnityEngine;
using System.Collections;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Randomizations;
using GeneticSharp.Domain.Fitnesses;
using System.Linq;
using Skahal.GameObjects;

public class DemoPlayerFitness : IFitness {
	#region IFitness implementation

	public double Evaluate (IChromosome chromosome)
	{
		var c = chromosome as DemoPlayerChromosome;
		var cannon = Cannon.Instance;

		var hit = Physics2D.BoxCastAll(
			cannon.transform.position, 
			cannon.GetComponent<BoxCollider2D>().size,
			0,
			new Vector2 (cannon.transform.position.x, 3));

		//Debug.DrawLine (cannon.transform.position, new Vector2 (cannon.transform.position.x, 3));

		var aliensCount = hit.Count (h => h.collider.IsAlien ());
		var ovniCount = hit.Count (h => h.collider.IsOvni ());
		var projectilesCount = hit.Count (h => h.collider.IsProjectile ());

//		if (aliensCount > 0) {
//			Debug.LogFormat ("AliensCount: {0}", aliensCount);
//		}

		//var newCannonPosition = cannon.GetNewPosition (c.HorizontalDirection);

		//var distance = SHGameObjectHelper.GetLowestDistanceFrom (newCannonPosition, Game.Instance.AliensWave.Aliens.Select (a => a.transform.position));

		//Debug.LogFormat ("Distance: {0}", distance);
		//var result = 1f - (distance / 10f);
		var result = 1f;

		if (aliensCount == 0 && c.HorizontalDirection == 0) {
			result = 0f;
		}

		if (projectilesCount > 0) {
			if (c.HorizontalDirection == 0) {
				result = 0;
			} else if(c.HorizontalDirection != PlayerInput.Instance.HorizontalDirection) {
				result = 0.5f;
			}
		}

		if (!c.IsShooting && aliensCount > 0) {
			result = 0f;
		}

		return result < 0 ? 0 : result;
	}

	#endregion


}
