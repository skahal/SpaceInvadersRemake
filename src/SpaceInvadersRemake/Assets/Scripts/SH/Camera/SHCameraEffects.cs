using UnityEngine;
 
[AddComponentMenu("Skahal/Camera/Camera Effects")]
public class SHCameraEffects : MonoBehaviour
{
	#region Campos 
	private static SHCameraEffects s_instance;
	#endregion
	
	#region Construtores
	public SHCameraEffects()
	{
		s_instance = this;
	}
	#endregion
	
	#region Ciclo de vida
	void Awake()
	{
		DontDestroyOnLoad(this);
	}
	#endregion
	
	#region MoveFromLeft
	public static void SlideFromLeft(float time)
	{
		SlideFromLeft(time, iTween.EaseType.linear);
	}
	
	public static void SlideFromLeft(float time, iTween.EaseType easing)
	{
		iTweenHelper.ValueTo(s_instance.gameObject, 
			iT.ValueTo.from, 0.1f,
			iT.ValueTo.to, 1f,
			iT.ValueTo.time, time,
			iT.ValueTo.easetype, easing,
			iT.ValueTo.onupdate, "SlideFromLeftOnUpdate");
	}
	
	void SlideFromLeftOnUpdate(float current)
	{
		Camera.main.rect = new Rect(0, 0, current, 1);
	}
	#endregion
}
