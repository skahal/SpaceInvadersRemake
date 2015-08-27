using UnityEngine;
using System.Collections;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Randomizations;

public class DemoPlayerChromosome : ChromosomeBase {

	public DemoPlayerChromosome() : base(2) {

		// Test 01
		ReplaceGene (0, GenerateGene (0)); // HorizontalDirection
		ReplaceGene (1, GenerateGene (1)); // IsShooting;

		//TODO: Should the chromosome has more genes?

		// Test 02
		// TargetAlien?
		// TargetX?
		// ShootOnAlienProjectile?
		// TargetOvni?
		// AvoidProjectiles?

		// Test 03
		// ShootOvniProbability
		// ShootAlienProbability(index);
		// StayBehindBunkerProbability(index);
		// AvoidAliensProjectilesProbability
	}

	public float HorizontalDirection { 
		get {
			return (int)GetGene (0).Value;
		}
	}
	public bool IsShooting {
		get {
			return (bool)GetGene (1).Value;
		}
	}

	#region implemented abstract members of ChromosomeBase

	public override Gene GenerateGene (int geneIndex)
	{
		if (geneIndex == 0) {
			return new Gene (RandomizationProvider.Current.GetInt(-1, 2));
		} else {
			return new Gene (RandomizationProvider.Current.GetInt (0, 2) == 0);
		}
	}

	public override IChromosome CreateNew ()
	{
		return new DemoPlayerChromosome ();
	}
		
	#endregion

}
