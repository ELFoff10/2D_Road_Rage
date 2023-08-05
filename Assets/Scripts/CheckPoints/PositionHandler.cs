using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer;

public class PositionHandler : MonoBehaviour
{
	[Inject]
	private ICoreStateMachine _coreStateMachine;

	public List<CarLapCounter> CarLapCounters = new List<CarLapCounter>();

	private LeaderBoardUIHandler _leaderBoardUIHandler;

	private void Start()
	{
		var carLapCounterArray = FindObjectsOfType<CarLapCounter>();

		CarLapCounters = carLapCounterArray.ToList<CarLapCounter>();

		foreach (var lapCounters in CarLapCounters)
		{
			lapCounters.OnPassCheckPoint += OnPassCheckPoint;
			lapCounters.OnPassTrainingCheckPoint1 += OnPassTrainingCheckPoint1;
			lapCounters.OnPassTrainingCheckPoint2 += OnPassTrainingCheckPoint2;
			lapCounters.OnPassTrainingCheckPoint3 += OnPassTrainingCheckPoint3;
		}

		_leaderBoardUIHandler = FindObjectOfType<LeaderBoardUIHandler>();

		if (_leaderBoardUIHandler != null)
		{
			_leaderBoardUIHandler.UpdateList(CarLapCounters);
		}
	}

	private void OnDestroy()
	{
		foreach (var lapCounters in CarLapCounters)
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
		CarLapCounters = CarLapCounters.OrderByDescending(s => s.GetNumberOfCheckPointPassed())
			.ThenBy(s => s.GetTimeAtLastCheckPoint()).ToList();

		// Get the cars position
		var carPosition = CarLapCounters.IndexOf(carLapCounter) + 1;

		// Tell the lap counter which position the car has
		carLapCounter.SetCarPosition(carPosition);

		if (_leaderBoardUIHandler != null)
		{
			_leaderBoardUIHandler.UpdateList(CarLapCounters);
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