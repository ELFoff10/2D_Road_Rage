using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer;

public class PositionHandler : MonoBehaviour
{
	public List<CarLapCounter> CarLapCounters = new List<CarLapCounter>();
	private LeaderBoardUIHandler _leaderBoardUIHandler;
	[Inject]
	private ICoreStateMachine _coreStateMachine;

	private void Start()
	{
		CarLapCounter[] carLapCounterArray = FindObjectsOfType<CarLapCounter>();
		CarLapCounters = carLapCounterArray.ToList<CarLapCounter>();

		foreach (CarLapCounter lapCounters in CarLapCounters)
		{
			lapCounters.OnPassCheckPoint += OnPassCheckPoint;
			lapCounters.OnPassTrainigCheckPoint1 += OnPassTrainingCheckPoint1;
			lapCounters.OnPassTrainigCheckPoint2 += OnPassTrainingCheckPoint2;
			lapCounters.OnPassTrainigCheckPoint3 += OnPassTrainingCheckPoint3;
		}

		_leaderBoardUIHandler = FindObjectOfType<LeaderBoardUIHandler>();

		if (_leaderBoardUIHandler != null)
		{
			_leaderBoardUIHandler.UpdateList(CarLapCounters);
		}
	}

	private void OnDestroy()
	{
		foreach (CarLapCounter lapCounters in CarLapCounters)
		{
			lapCounters.OnPassCheckPoint -= OnPassCheckPoint;
			lapCounters.OnPassTrainigCheckPoint1 -= OnPassTrainingCheckPoint1;
		}
	}

	private void OnPassCheckPoint(CarLapCounter carLapCounter)
	{
		// Sort the cars position first based on how many checkpoints they have passed, more is always better. Then sort on time where shorter time os better
		CarLapCounters = CarLapCounters.OrderByDescending(s => s.GetNumberOfCheckPointPassed())
			.ThenBy(s => s.GetTimeAtLastCheckPoint()).ToList();

		// Get the cars position
		int carPosition = CarLapCounters.IndexOf(carLapCounter) + 1;

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