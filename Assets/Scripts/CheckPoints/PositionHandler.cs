using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer;

public class PositionHandler : MonoBehaviour
{
	private List<CarLapCounter> _carLapCounters = new List<CarLapCounter>();
	private LeaderBoardUIHandler _leaderBoardUIHandler;

	[Inject]
	private ICoreStateMachine _coreStateMachine;

	private void Start()
	{
		var carLapCounterArray = FindObjectsOfType<CarLapCounter>();

		if (_coreStateMachine.ScenesState.Value is ScenesStateEnum.Level4 or ScenesStateEnum.Level5)
		{
			foreach (var carLapCounter in carLapCounterArray)
			{
				carLapCounter.LapsToComplete = 2;
			}
		}

		_carLapCounters = carLapCounterArray.ToList<CarLapCounter>();

		foreach (var lapCounters in _carLapCounters)
		{
			lapCounters.OnPassCheckPoint += OnPassCheckPoint;
			lapCounters.OnPassTrainingCheckPoint1 += OnPassTrainingCheckPoint1;
			lapCounters.OnPassTrainingCheckPoint2 += OnPassTrainingCheckPoint2;
			lapCounters.OnPassTrainingCheckPoint3 += OnPassTrainingCheckPoint3;
		}

		_leaderBoardUIHandler = FindObjectOfType<LeaderBoardUIHandler>();

		if (_leaderBoardUIHandler != null)
		{
			_leaderBoardUIHandler.UpdateList(_carLapCounters);
		}
	}

	private void OnDestroy()
	{
		foreach (var lapCounters in _carLapCounters)
		{
			lapCounters.OnPassCheckPoint -= OnPassCheckPoint;
			lapCounters.OnPassTrainingCheckPoint1 -= OnPassTrainingCheckPoint1;
			lapCounters.OnPassTrainingCheckPoint2 -= OnPassTrainingCheckPoint2;
			lapCounters.OnPassTrainingCheckPoint3 -= OnPassTrainingCheckPoint3;
		}
	}

	private void OnPassCheckPoint(CarLapCounter carLapCounter)
	{
		// Sort the cars position first based on how many checkpoints they have passed, more is always better. Then sort on time where shorter time os better
		_carLapCounters = _carLapCounters.OrderByDescending(s => s.GetNumberOfCheckPointPassed())
			.ThenBy(s => s.GetTimeAtLastCheckPoint()).ToList();

		// Get the cars position
		var carPosition = _carLapCounters.IndexOf(carLapCounter) + 1;

		// Tell the lap counter which position the car has
		carLapCounter.SetCarPosition(carPosition);

		if (_leaderBoardUIHandler != null)
		{
			_leaderBoardUIHandler.UpdateList(_carLapCounters);
		}
	}

	private void OnPassTrainingCheckPoint1(CarLapCounter carLapCounter)
	{
		_coreStateMachine.LevelGameStateMachine.SetGameState(GameStateEnum.TrainingCheckPoint1);
	}

	private void OnPassTrainingCheckPoint2(CarLapCounter carLapCounter)
	{
		_coreStateMachine.LevelGameStateMachine.SetGameState(GameStateEnum.TrainingCheckPoint2);
	}

	private void OnPassTrainingCheckPoint3(CarLapCounter carLapCounter)
	{
		_coreStateMachine.LevelGameStateMachine.SetGameState(GameStateEnum.TrainingCheckPoint3);
	}
}