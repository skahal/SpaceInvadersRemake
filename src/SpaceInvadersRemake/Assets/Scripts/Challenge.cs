using UnityEngine;
using System.Collections;
using Vexe.Runtime.Types;

public class Challenge : BetterBehaviour {
	public ChallengeProperty AlienShootInterval;
	public ChallengeProperty AlienShootProbability;
	public ChallengeProperty OvniSpeed;

	void Start() {
		var game = Game.Instance;
		var ovni = Ovni.Instance;
		var level = game.WaveNumber - 1;

		game.AlienShootInterval = AlienShootInterval.GetLevelValue (level);
		game.AlienShootProbability = AlienShootProbability.GetLevelValue (level);
		ovni.Speed = OvniSpeed.GetLevelValue (level);

		Debug.LogFormat (
			"Challenge changed to AlienShootInterval:{0}|AlienShootProbability:{1}|OvniSpeed:{2}",
			game.AlienShootInterval,
			game.AlienShootProbability,
			ovni.Speed);
	}
}
