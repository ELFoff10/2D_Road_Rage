using System;
using Tools.UiManager;
using Ui.Windows;
using UniRx;
using VContainer.Unity;

namespace RoadRage.Controllers
{
    public class LevelControllerModel : IInitializable, IDisposable
    {
        private readonly ICoreStateMachine _coreStateMachine;
        private readonly IWindowManager _windowManager;
        private CompositeDisposable _compositeDisposable = new CompositeDisposable();

        public LevelControllerModel(ICoreStateMachine coreStateMachine, IWindowManager windowManager)
        {
            _coreStateMachine = coreStateMachine;
            _windowManager = windowManager;
        }
        public void Initialize()
        {
            _coreStateMachine.SceneEndLoadFade += OnSceneEndLoad;
            _coreStateMachine.LevelGameStateMachine.GameState.SkipLatestValueOnSubscribe().Subscribe(GameStateChange)
                .AddTo(_compositeDisposable);
        }

        public void Dispose()
        {
            _coreStateMachine.SceneEndLoadFade -= OnSceneEndLoad;
            _compositeDisposable.Clear();
        }

        #region Subs

        private void GameStateChange(GameStateEnum gameState)
        {
            switch (gameState)
            {
                case GameStateEnum.None:
                    break;
                case GameStateEnum.PrePlay:
                    break;
                case GameStateEnum.CountDown:
                    _windowManager.Show<GameMenuWindow>();
                    break;
                case GameStateEnum.Play:
                    break;
                case GameStateEnum.Dead:
                    break;
                case GameStateEnum.RaceOver:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(gameState), gameState, null);
            }
        }

        #endregion
        #region SceneLoad

        private void OnSceneEndLoad(ScenesStateEnum scenesStateEnum)
        {
            _coreStateMachine.SceneEndLoadFade -= OnSceneEndLoad;
            // _coreStateMachine.GameStateMachine.SetGameState(GameStateEnum.Play);
        }

        #endregion
        
    }
}