using System;
using System.Collections.Generic;
using UniRx;

public interface ITimerService
{
    void AddGameTimer(int currentSec, Action<int> timeModelTick,
        Action timeModelEndEvent, bool ignoreTimeScale = true);

    void AddDefaultTimer(int currentSec, Action<int> timeModelTick, Action timeModelEndEvent);
    void RestartGameTimers();
    void RestartDefaultTimers();
}

public class TimerService : ITimerService
{
    private List<TimerModel> _gameTimeModels = new List<TimerModel>();
    private List<TimerModel> _defaultTimeModels = new List<TimerModel>();

    private List<TimerModel> _tmpTimeModels = new List<TimerModel>();
    private readonly ICoreStateMachine _coreStateMachine;

    public TimerService(ICoreStateMachine coreStateMachine)
    {
        _coreStateMachine = coreStateMachine;
        _coreStateMachine.RunTimeState.Subscribe(OnRunTime);
    }

    private void OnRunTime(RunTimeStateEnum runTimeState)
    {
        switch (runTimeState)
        {
            case RunTimeStateEnum.Play:
                foreach (var gameTimeModels in _gameTimeModels)
                {
                    gameTimeModels.StopTick();
                    gameTimeModels.StartTick();
                }

                break;
            case RunTimeStateEnum.Pause:
                foreach (var gameTimeModels in _gameTimeModels)
                {
                    gameTimeModels.StopTick();
                }

                break;
        }
    }

    public void Restart()
    {
        RestartGameTimers();
    }

    public void AddGameTimer(int currentSec, Action<int> timeModelTick, Action timeModelEndEvent,
        bool ignoreTimeScale = true)
    {
        if (_tmpTimeModels.Count >= 1)
        {
            _tmpTimeModels[0].Init(currentSec, TimerTypeEnum.Game, timeModelTick, timeModelEndEvent,
                ignoreTimeScale);

            if (_coreStateMachine.RunTimeState.Value == RunTimeStateEnum.Play)
            {
                _tmpTimeModels[0].StartTick();
            }

            _gameTimeModels.Add(_tmpTimeModels[0]);
            _tmpTimeModels.RemoveAt(0);
        }
        else
        {
            TimerModel timeModel = new TimerModel(this, currentSec, TimerTypeEnum.Game, timeModelTick,
                timeModelEndEvent, ignoreTimeScale);
            if (_coreStateMachine.RunTimeState.Value == RunTimeStateEnum.Play)
            {
                timeModel.StartTick();
            }

            _gameTimeModels.Add(timeModel);
        }
    }

    public void AddDefaultTimer(int currentSec, Action<int> timeModelTick, Action timeModelEndEvent)
    {
        if (_tmpTimeModels.Count >= 1)
        {
            _tmpTimeModels[0].Init(currentSec, TimerTypeEnum.Default, timeModelTick, timeModelEndEvent, true);
            _tmpTimeModels[0].StartTick();
            _defaultTimeModels.Add(_tmpTimeModels[0]);
            _tmpTimeModels.RemoveAt(0);
        }
        else
        {
            TimerModel timeModel = new TimerModel(this, currentSec, TimerTypeEnum.Default, timeModelTick,
                timeModelEndEvent, true);
            timeModel.StartTick();
            _defaultTimeModels.Add(timeModel);
        }
    }

    public void RestartGameTimers()
    {
        foreach (var gameTimeModel in _gameTimeModels)
        {
            gameTimeModel.RestartTick();
            _tmpTimeModels.Add(gameTimeModel);
        }

        _gameTimeModels.Clear();
    }

    public void RestartDefaultTimers()
    {
        foreach (var defaultTimeModel in _defaultTimeModels)
        {
            defaultTimeModel.RestartTick();
            _tmpTimeModels.Add(defaultTimeModel);
        }

        _defaultTimeModels.Clear();
    }

    public void RemoveTimer(TimerTypeEnum timerTypeEnum, TimerModel timeModel)
    {
        switch (timerTypeEnum)
        {
            case TimerTypeEnum.Game:
                _gameTimeModels.Remove(timeModel);
                _tmpTimeModels.Add(timeModel);
                break;
            case TimerTypeEnum.Default:
                _defaultTimeModels.Remove(timeModel);
                _tmpTimeModels.Add(timeModel);
                break;
        }
    }
}