using System;
using Enums;
using RoadRage.StateMachines.Models;
using UniRx;

namespace RoadRage.StateMachines
{
    public interface ICoreStateMachine
    {
        void SetRunTimeState(RunTimeStateEnum runTimeStateEnum);
        void SetScenesState(ScenesStateEnum scenesStateEnum);
        void OnSceneEndLoadFade();
        void OnSceneEndLoad();
        IReadOnlyReactiveProperty<RunTimeStateEnum> RunTimeState { get; }
        IReadOnlyReactiveProperty<ScenesStateEnum> ScenesState { get; }
        RunTimeStateEnum LastRunTimeState { get; }

        event Action<ScenesStateEnum> SceneEndLoadFade;

        event Action<ScenesStateEnum> SceneEndLoad;
        
        public ILevelGameStateMachine LevelGameStateMachine { get; }
    }

    public class CoreStateMachine : ICoreStateMachine
    {
        public event Action ResetProgress;

        #region Enums

        private ReactiveProperty<RunTimeStateEnum> _runTimeState =
            new ReactiveProperty<RunTimeStateEnum>(RunTimeStateEnum.Pause);

        private ReactiveProperty<ScenesStateEnum> _scenesState =
            new ReactiveProperty<ScenesStateEnum>(ScenesStateEnum.Base);

        public IReadOnlyReactiveProperty<RunTimeStateEnum> RunTimeState => _runTimeState;
        public IReadOnlyReactiveProperty<ScenesStateEnum> ScenesState => _scenesState;
        public RunTimeStateEnum LastRunTimeState { get; private set; }

        public event Action<ScenesStateEnum> SceneEndLoadFade;

        public event Action<ScenesStateEnum> SceneEndLoad;

        #endregion

        #region SubStates
        private LevelGameStateMachine _levelGameStateMachine = new LevelGameStateMachine();
        public ILevelGameStateMachine LevelGameStateMachine => _levelGameStateMachine;
        

        #endregion

        public CoreStateMachine()
        {
            LastRunTimeState = RunTimeStateEnum.Pause;
        }

        #region SetStates

        public void SetRunTimeState(RunTimeStateEnum runTimeStateEnum)
        {
            LastRunTimeState = _runTimeState.Value;
            _runTimeState.Value = runTimeStateEnum;
        }

        public void SetScenesState(ScenesStateEnum scenesStateEnum)
        {
            _scenesState.Value = scenesStateEnum;
        }

        public void OnSceneEndLoadFade()
        {
            SceneEndLoadFade?.Invoke(_scenesState.Value);
        }

        public void OnSceneEndLoad()
        {
            SceneEndLoad?.Invoke(_scenesState.Value);
        }

        #endregion

        public void OnResetProgress() => ResetProgress?.Invoke();
    }
}