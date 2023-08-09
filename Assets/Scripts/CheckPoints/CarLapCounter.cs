using System;
using System.Collections;
using UnityEngine;
using VContainer;

public class CarLapCounter : MonoBehaviour
{
	[Inject]
	private readonly ICoreStateMachine _coreStateMachine;
	[Inject]
	private readonly AudioManager _audioManager;
	[Inject]
	private readonly FMOD_Events _fmodEvents;
	[Inject]
	private readonly GameEventsManager _gameEventsManager;

	public event Action<CarLapCounter> OnPassCheckPoint;
	// public event Action<CarLapCounter> OnFinishCheckPoint;
	public event Action<CarLapCounter> OnPassTrainingCheckPoint1;
	public event Action<CarLapCounter> OnPassTrainingCheckPoint2;
	public event Action<CarLapCounter> OnPassTrainingCheckPoint3;

	private int _passedCheckPointNumber;
	private float _timeAtLastPassedCheckPoint;
	private int _numberOfPassedCheckPoints;
	private int _lapsCompleted;
	public int LapsToComplete = 1;
	private bool _isRaceCompleted;
	private int _carPosition;
	private bool _isHideRoutineRunning;
	private float _hideUIDelayTime;

	public void SetCarPosition(int position)
	{
		_carPosition = position;
	}

	public int GetNumberOfCheckPointPassed()
	{
		return _numberOfPassedCheckPoints;
	}

	public float GetTimeAtLastCheckPoint()
	{
		return _timeAtLastPassedCheckPoint;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!collision.CompareTag("CheckPoint")) return;

		if (_isRaceCompleted)
		{
			return;
		}

		var checkPoint = collision.GetComponent<CheckPoint>();

		if (_passedCheckPointNumber + 1 != checkPoint.CheckPointNumber) return;

		_passedCheckPointNumber = checkPoint.CheckPointNumber;

		_numberOfPassedCheckPoints++;

		_timeAtLastPassedCheckPoint = Time.time;

		if (checkPoint.IsFinishLine)
		{
			_gameEventsManager.FinishLinePassed();
            
			_passedCheckPointNumber = 0;
			_lapsCompleted++;

			if (_lapsCompleted >= LapsToComplete)
			{
				_isRaceCompleted = true;
			}
		}

		if (checkPoint.IsTrainingPause1)
		{
			OnPassTrainingCheckPoint1?.Invoke(this);
		}

		if (checkPoint.IsTrainingPause2)
		{
			OnPassTrainingCheckPoint2?.Invoke(this);
		}

		if (checkPoint.IsTrainingPause3)
		{
			OnPassTrainingCheckPoint3?.Invoke(this);
		}

		OnPassCheckPoint?.Invoke(this);

		if (_isRaceCompleted == true)
		{
			StartCoroutine(ShowPositionCo(300));

			if (!CompareTag("Player")) return;
			// OnFinishCheckPoint?.Invoke(this);
			_audioManager.EventInstances[(int)AudioNameEnum.Finish].start();
			_coreStateMachine.LevelGameStateMachine.SetGameState(GameStateEnum.RaceOver);
			GetComponent<CarInputHandler>().enabled = false;
			GetComponent<CarAIHandler>().enabled = true;
		}
		else if (checkPoint.IsFinishLine)
		{
			StartCoroutine(ShowPositionCo(1.5f));
		}
	}

	private IEnumerator ShowPositionCo(float delayUntilHidePosition)
	{
		_hideUIDelayTime += delayUntilHidePosition;

		if (_isHideRoutineRunning) yield break;

		_isHideRoutineRunning = true;

		yield return new WaitForSeconds(_hideUIDelayTime);

		_isHideRoutineRunning = false;
	}
}