using System.Collections;

public interface IPlayerInputStrategy 
{
	float HorizontalDirection { get; }
	bool IsShooting { get; }
	bool IsRestart { get; }
	bool IsQuit { get; }
}