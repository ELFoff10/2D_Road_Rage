using System;
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
		_coreStateMachine.LevelGameStateMachine.GameState/*.SkipLatestValueOnSubscribe()*/.Subscribe(GameStateChange)
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
				_windowManager.Hide<GameMenuWindow>();
				_windowManager.Show<BackgroundMenuWindow>();
				_windowManager.Show<MainMenuWindow>();
				break;
			case GameStateEnum.TrainingCheckPoint1:
				break;
			case GameStateEnum.CountDown:
				_windowManager.Hide<BackgroundMenuWindow>();
				_windowManager.Hide<MainMenuWindow>();
				_windowManager.Show<GameMenuWindow>();
				break;
			case GameStateEnum.Play:
				break;
			case GameStateEnum.Dead:
				break;
			case GameStateEnum.RaceOver:
				break;
		}
	}

	#endregion

	#region SceneLoad

	private void OnSceneEndLoad(ScenesStateEnum scenesStateEnum)
	{
		_coreStateMachine.SceneEndLoadFade -= OnSceneEndLoad;
		_coreStateMachine.LevelGameStateMachine.SetGameState(GameStateEnum.CountDown);
	}

	#endregion
}