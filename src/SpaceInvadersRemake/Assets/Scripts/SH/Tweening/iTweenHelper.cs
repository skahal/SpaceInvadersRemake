#region Usings
using UnityEngine;
using System.Collections;
 
#endregion

public static class iTweenHelper
{
	private static Hashtable CreateHashParams(params object[] argsKeyValue)
	{
		Hashtable args = new Hashtable();
		
		for (int i = 0; i < argsKeyValue.Length; i += 2)
		{
			args.Add(argsKeyValue[i], argsKeyValue[i + 1]);
		}
		
		return args;
	}
	
	#region Move
	public static void MoveTo(GameObject target, params object[] argsKeyValue)
	{		
		iTween.MoveTo(target, CreateHashParams(argsKeyValue));
	}
	
	public static void MoveFrom(GameObject target, params object[] argsKeyValue)
	{
		iTween.MoveFrom(target, CreateHashParams(argsKeyValue));
	}
	#endregion
	
	#region Scale
	public static void ScaleFrom(GameObject target, params object[] argsKeyValue)
	{
		iTween.ScaleFrom(target, CreateHashParams(argsKeyValue));
	}
	
	public static void ScaleTo(GameObject target, params object[] argsKeyValue)
	{
		iTween.ScaleTo(target, CreateHashParams(argsKeyValue));
	}
	#endregion
	
	#region Color
	public static void ColorTo(GameObject target, params object[] argsKeyValue)
	{
		iTween.ColorTo(target, CreateHashParams(argsKeyValue));
	}
	
	public static void ColorFrom(GameObject target, params object[] argsKeyValue)
	{
		iTween.ColorFrom(target, CreateHashParams(argsKeyValue));
	}
	#endregion
	
	#region Shake
	public static void ShakePosition(GameObject target, params object[] argsKeyValue)
	{
		iTween.ShakePosition(target, CreateHashParams(argsKeyValue));
	}
	
	public static void ShakeScale(GameObject target, params object[] argsKeyValue)
	{
		iTween.ShakeScale(target, CreateHashParams(argsKeyValue));
	}
	
	public static void ShakeRotation(GameObject target, params object[] argsKeyValue)
	{
		iTween.ShakeRotation(target, CreateHashParams(argsKeyValue));
	}
	#endregion
	
	#region Ratation
	public static void RotateTo(GameObject target, params object[] argsKeyValue)
	{
		iTween.RotateTo(target, CreateHashParams(argsKeyValue));
	}
	
	public static void RotateFrom(GameObject target, params object[] argsKeyValue)
	{
		iTween.RotateFrom(target, CreateHashParams(argsKeyValue));
	}
	
	public static void RotateBy(GameObject target, params object[] argsKeyValue)
	{
		iTween.RotateBy(target, CreateHashParams(argsKeyValue));
	}
	#endregion
	
	#region Value
	public static void ValueTo(GameObject target, params object[] argsKeyValue)
	{
		iTween.ValueTo(target, CreateHashParams(argsKeyValue));
	}
	#endregion
	
	#region Camera
	public static void CameraFadeTo(params object[] argsKeyValue)
	{
		iTween.CameraFadeTo(CreateHashParams(argsKeyValue));
	}
	
	public static void CameraFadeFrom(params object[] argsKeyValue)
	{
		iTween.CameraFadeFrom(CreateHashParams(argsKeyValue));
	}
	#endregion
}

