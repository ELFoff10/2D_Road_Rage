using UnityEngine;
using TMPro;
using VContainer;

public class LifeCountUI : MonoBehaviour
{
	[SerializeField] 
	private TMP_Text _lifeCountText;
	public int LifeCount = 3;

	[Inject]
	private readonly GameEventsManager _gameEventsManager;
	[Inject]
	private readonly ICoreStateMachine _coreStateMachine;
	
	private void OnEnable()
	{
		_gameEventsManager.OnPlayerBreakBarrier += OnPlayerBreakBarrier;
	}

	private void OnDisable()
	{
		_gameEventsManager.OnPlayerBreakBarrier -= OnPlayerBreakBarrier;
	}

	private void OnPlayerBreakBarrier()
	{
		LifeCount--;
		_lifeCountText.text = LifeCount.ToString();
		
		if (LifeCount == 0)
		{
			_coreStateMachine.LevelGameStateMachine.SetGameState(GameStateEnum.RaceOver);
			LifeCount = 3;
			_lifeCountText.text = LifeCount.ToString();
		}
	}
}