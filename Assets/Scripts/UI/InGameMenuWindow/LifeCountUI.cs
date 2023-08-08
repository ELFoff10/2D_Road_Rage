using UnityEngine;
using TMPro;
using VContainer;

public class LifeCountUI : MonoBehaviour
{
	[SerializeField] 
	private TMP_Text _lifeCountText;
	[SerializeField] 
	private int _lifeCount = 3;

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
		_lifeCount--;
		_lifeCountText.text = _lifeCount.ToString();
		
		if (_lifeCount == 0)
		{
			_coreStateMachine.LevelGameStateMachine.SetGameState(GameStateEnum.RaceOver);
			_lifeCount = 3;
			_lifeCountText.text = _lifeCount.ToString();
		}
	}
}