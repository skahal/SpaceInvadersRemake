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

	
		var aliensCount = hit.Count (h => h.collider.IsAlien ());
		var ovniCount = hit.Count (h => h.collider.IsOvni ());
		var projectilesCount = hit.Count (h => h.collider.IsProjectile ());
		var bunkersCount = hit.Count (h => h.collider.IsBunker ());

		var result = 0f;

		if (aliensCount > 0) {
			result += c.ShootAlienProbability / 4f;
		}

		if (ovniCount > 0) {
			result += c.ShootOvniProbability / 4f;
		}

		if (projectilesCount > 0) {
			result += c.AvoidAliensProjectilesProbability / 4f;
		}

		if (bunkersCount > 0) {
			result += c.StayBehindBunkerProbability / 4f;
		}

		return result;
	}

	#endregion


}
