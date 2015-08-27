using System.Collections;
using UnityEngine;

public class KeyboardPlayerInputStrategy : IPlayerInputStrategy
{
	public float HorizontalDirection
	{
		get {
			return Input.GetAxisRaw ("Horizontal");
		}
	}

	public bool IsShooting
	{
		get {
			return Input.GetKeyDown (KeyCode.X) || Input.GetKeyDown (KeyCode.Space);
		}
	}

	public bool IsRestart
	{
		get {
			return Input.GetKeyDown (KeyCode.Return);
		}
	}

	public bool IsQuit
	{
		get {
			return Input.GetKeyDown (KeyCode.Escape);
		}
	}
}