using System.Collections;
using UnityEngine;
using GeneticSharp.Domain;
using GeneticSharp.Domain.Populations;
using GeneticSharp.Domain.Selections;
using GeneticSharp.Domain.Crossovers;
using GeneticSharp.Domain.Mutations;
using GeneticSharp.Domain.Terminations;
using Skahal.Threading;

public class DemoPlayerInputStrategy : IPlayerInputStrategy
{
	private GeneticAlgorithm m_ga;
	private DemoPlayerChromosome m_bestChromossome;
	private bool m_restart;

	public DemoPlayerInputStrategy() 
	{
		var population = new Population (6, 6, new DemoPlayerChromosome ());
		var fitness = new DemoPlayerFitness ();
		var selection = new EliteSelection ();
		var crossover = new OnePointCrossover (0);
		var mutation = new UniformMutation (true);

		m_ga = new GeneticAlgorithm (
			population,
			fitness,
			selection,
			crossover, 
			mutation);

		m_ga.MutationProbability = 0.5f;
		m_ga.GenerationRan += (sender, e) => {
			m_bestChromossome = m_ga.BestChromosome as DemoPlayerChromosome;

			if(m_bestChromossome.AvoidAliensProjectilesProbability > 0.9f) {
				HorizontalDirection = 1f;
			}
		};
		m_ga.Start ();

		SHCoroutine.PingPong (.01f, 0, 1, (t) => {
			m_ga.Termination = new GenerationNumberTermination (m_ga.GenerationsNumber + 1);
			m_ga.Resume();

			return true;
		});
	}

	public float HorizontalDirection { get; private set; }

	public bool IsShooting { get; private set; }

	public bool IsRestart
	{
		get {
			if (m_restart) {
				m_restart = false;
				return true;
			}

			return false;
		}
	}

	public bool IsQuit
	{
		get {
			return false;
		}
	}
}