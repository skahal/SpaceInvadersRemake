using UnityEngine;
using System.Collections;

public class Pixel
{
	public Pixel(int x, int y, Color32 color)
	{
		X = x;
		Y = y;
		Color = color;
	}

	public int X;
	public int Y;
	public Color Color;
}
