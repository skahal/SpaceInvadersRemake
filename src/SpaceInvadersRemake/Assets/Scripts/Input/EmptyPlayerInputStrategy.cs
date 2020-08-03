public class EmptyPlayerInputStrategy : IPlayerInputStrategy
{
    public float HorizontalDirection => 0;

    public bool IsShooting => false;

    public bool IsRestart => false;

    public bool IsQuit => false;
}