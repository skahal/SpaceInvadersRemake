using UnityEngine;
using System.Collections;

public static class ComponentExtensions {

	public static bool IsAlien(this Component component) 
	{
		return component.CompareTag("Alien");
	}

	public static bool IsHorizontalEdge(this Component component) 
	{
		return component.CompareTag("HorizontalEdge");
	}

	public static bool IsVerticalEdge(this Component component) 
	{
		return component.CompareTag("VerticalEdge");
	}

	public static bool IsProjectile(this Component component) 
	{
		return component.CompareTag("Projectile");
	}
}
