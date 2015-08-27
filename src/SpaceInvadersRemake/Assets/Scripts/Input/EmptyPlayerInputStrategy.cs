using System.Collections;
using UnityEngine;

public class EmptyPlayerInputStrategy : IPlayerInputStrategy
{
	public float HorizontalDirection
	{
		get {
			return 0;
		}
	}

	public bool IsShooting
	{
		get {
			return false;
		}
	}

	public bool IsRestart
	{
		get {
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