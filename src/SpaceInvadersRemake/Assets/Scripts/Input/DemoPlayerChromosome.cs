using UnityEngine;
using System.Collections;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Randomizations;

public class DemoPlayerChromosome : ChromosomeBase {

	public DemoPlayerChromosome() : base(4) {

		// Test 02
		// TargetAlien?
		// TargetX?
		// ShootOnAlienProjectile?
		// TargetOvni?
		// AvoidProjectiles?

		// Test 03
		ReplaceGene (0, GenerateGene (0)); // ShootOvniProbability
		ReplaceGene (1, GenerateGene (1)); // ShootAlienProbability(index);
		ReplaceGene (2, GenerateGene (2)); // StayBehindBunkerProbability(index);
		ReplaceGene (3, GenerateGene (3)); // AvoidAliensProjectilesProbability
	}

	public float ShootOvniProbability { 
		get {
			return (float)GetGene (0).Value;
		}
	}

	public float ShootAlienProbability { 
		get {
			return (float)GetGene (1).Value;
		}
	}

	public float StayBehindBunkerProbability { 
		get {
			return (float)GetGene (2).Value;
		}
	}

	public float AvoidAliensProjectilesProbability { 
		get {
			return (float)GetGene (3).Value;
		}
	}

	#region implemented abstract members of ChromosomeBase

	public override Gene GenerateGene (int geneIndex)
	{
		return new Gene(RandomizationProvider.Current.GetFloat ());
	}

	public override IChromosome CreateNew ()
	{
		return new DemoPlayerChromosome ();
	}
		
	#endregion

}
