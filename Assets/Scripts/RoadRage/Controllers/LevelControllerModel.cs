using System;
using Tools.UiManager;
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
		switch (_coreStateMachine.ScenesState.Value)
		{
			case ScenesStateEnum.TrainingLevel:
				if (gameState == GameStateEnum.CountDown)
				{
					HideMenuWindows();
					_windowManager.Show<GameWindowLevel0Training>();
				}

				break;
			case ScenesStateEnum.Level1:
				if (gameState == GameStateEnum.CountDown)
				{
					HideMenuWindows();
					_windowManager.Show<GameWindowLevel1>();
				}

				break;
			case ScenesStateEnum.Level2:
				if (gameState == GameStateEnum.CountDown)
				{
					HideMenuWindows();
					_windowManager.Show<GameWindowLevel2>();
				}

				break;
			case ScenesStateEnum.Level3:
				if (gameState == GameStateEnum.CountDown)
				{
					HideMenuWindows();
					_windowManager.Show<GameWindowLevel3>();
				}

				break;
			case ScenesStateEnum.Level4:
				if (gameState == GameStateEnum.CountDown)
				{
					HideMenuWindows();

					_windowManager.Show<GameWindowLevel4>();
				}

				break;
			case ScenesStateEnum.Level5:
				if (gameState == GameStateEnum.CountDown)
				{
					HideMenuWindows();
					_windowManager.Show<GameWindowLevel5>();
				}

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

	private void HideMenuWindows()
	{
		_windowManager.Hide<MenuBackgroundWindow>();
		_windowManager.Hide<MenuMainWindow>();
	}
}