using System;

[Serializable]
public class ChallengeProperty {
	public float StartValue;
	public float VariationByLevel;

	public float GetLevelValue(int level){
		return StartValue + (VariationByLevel * level);
	}
}
