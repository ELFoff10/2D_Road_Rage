using System;

public class GameEventsManager
{
	public event Action OnGemCollected;
	public event Action OnPlayerBreakBarrier;

	public void GemCollected()
	{
		OnGemCollected?.Invoke();
	}
	
	public void PlayerBreakBarrier()
	{
		OnPlayerBreakBarrier?.Invoke();
	}
}