using UnityEngine;

public class KeyboardPlayerInputStrategy : IPlayerInputStrategy
{
    public float HorizontalDirection => Input.GetAxisRaw("Horizontal");

    public bool IsShooting => Input.GetKey(KeyCode.X) || Input.GetKey(KeyCode.Space);

    public bool IsRestart => Input.GetKeyDown(KeyCode.Return);

    public bool IsQuit => Input.GetKeyDown(KeyCode.Escape);
}