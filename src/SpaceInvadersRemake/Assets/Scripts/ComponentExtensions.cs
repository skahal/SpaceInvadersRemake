﻿using UnityEngine;
using System.Collections;

public static class ComponentExtensions {

	public static bool IsAlien(this Component component) 
	{
		return component.CompareTag("Alien");
	}

	public static bool IsOvni(this Component component) 
	{
		return component.name.Equals ("Ovni");
	}

	public static bool IsHorizontalEdge(this Component component) 
	{
		return component.CompareTag("HorizontalEdge");
	}

	public static bool IsVerticalEdge(this Component component) 
	{
		return component.CompareTag("VerticalEdge");
	}
		
	public static bool IsAlienVerticalEdge(this Component component) 
	{
		return component.CompareTag("AlienVerticalEdge");
	}

	public static bool IsProjectile(this Component component) 
	{
		return component.CompareTag("Projectile");
	}

	public static bool IsCannonZone(this Component component) 
	{
		return component.CompareTag("CannonZone");
	}

	public static bool IsBunker(this Component component) 
	{
		return component.CompareTag("Bunker");
	}
}
