using System;
using FMOD;
using Tools.UiManager;
using Ui.Windows;
using UniRx;
using VContainer.Unity;

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
		_coreStateMachine.LevelGameStateMachine.GameState.Subscribe(GameStateChange)
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
		if (_coreStateMachine.ScenesState.Value == ScenesStateEnum.TrainingLevel)
		{
			switch (gameState)
			{
				case GameStateEnum.None:
					break;
				case GameStateEnum.TrainingCheckPoint1:
					break;
				case GameStateEnum.CountDown:
					_windowManager.Hide<MenuBackgroundWindow>();
					_windowManager.Hide<MenuMainWindow>();
					_windowManager.Show<GameTrainingLevelWindow>();
					break;
				case GameStateEnum.Play:
					break;
				case GameStateEnum.Dead:
					break;
				case GameStateEnum.RaceOver:
					break;
			}
		}
		else
		{
			switch (gameState)
			{
				case GameStateEnum.None:
					break;
				case GameStateEnum.TrainingCheckPoint1:
					break;
				case GameStateEnum.CountDown:
					_windowManager.Hide<MenuBackgroundWindow>();
					_windowManager.Hide<MenuMainWindow>();
					_windowManager.Show<GameWindow>();
					break;
				case GameStateEnum.Play:
					break;
				case GameStateEnum.Dead:
					break;
				case GameStateEnum.RaceOver:
					break;
			}
		}
	}

	#endregion

	#region SceneLoad

	private void OnSceneEndLoad(ScenesStateEnum scenesStateEnum)
	{
		_coreStateMachine.SceneEndLoadFade -= OnSceneEndLoad;
		// _coreStateMachine.LevelGameStateMachine.SetGameState(GameStateEnum.CountDown);
	}

	#endregion
}