using Enums;
using FMOD.Studio;
using RoadRage.MultiScene;
using Tools.UiManager;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;


public class GameMenuWindow : Window
{
	[Inject]
	private readonly ICoreStateMachine _coreStateMachine;

	[Inject]
	private readonly AudioManager _audioManager;

	[Inject]
	private readonly IMultiSceneManager _multiSceneManager;

	[SerializeField]
	private UiButton _menuButton;
	[SerializeField]
	private UiButton _exitToMenuButton;
	[SerializeField]
	private UiButton _raceAgainButton;
	[SerializeField]
	private GameObject _viewMenuUI;
	[SerializeField]
	private DistanceUIHandler _distanceUI;
	[SerializeField]
	private RaceTimeUIHandler _raceTimeUI;

	protected override void OnActivate()
	{
		base.OnActivate();
		if (_coreStateMachine.ScenesState.Value == ScenesStateEnum.Level4)
		{
			_raceTimeUI.gameObject.SetActive(false);
			_distanceUI.gameObject.SetActive(true);
		}
		else
		{
			_distanceUI.gameObject.SetActive(false);
		}

		_menuButton.OnClick += OnMenuButton;
		_raceAgainButton.OnClick += OnRaceAgainButton;
		_exitToMenuButton.OnClick += OnExitToMenuButton;
		_coreStateMachine.LevelGameStateMachine.OnSetGameState += ShowMenu;
	}

	protected override void OnDeactivate()
	{
		base.OnDeactivate();
		_viewMenuUI.gameObject.SetActive(false);
		_menuButton.OnClick -= OnMenuButton;
		_raceAgainButton.OnClick -= OnRaceAgainButton;
		_exitToMenuButton.OnClick -= OnExitToMenuButton;
		_coreStateMachine.LevelGameStateMachine.OnSetGameState -= ShowMenu;
		// _coreStateMachine.SceneEndLoad -= OnSceneEndLoad;
	}

	private void OnMenuButton()
	{
		_viewMenuUI.gameObject.SetActive(true);
		_coreStateMachine.LevelGameStateMachine.SetGameState(GameStateEnum.PrePlay);
		_audioManager.EventInstances[(int)AudioNameEnum.GameBackgroundMusic].stop(STOP_MODE.ALLOWFADEOUT);
		_audioManager.EventInstances[(int)AudioNameEnum.CarEngine].stop(STOP_MODE.IMMEDIATE);
	}

	private void OnRaceAgainButton()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		_viewMenuUI.gameObject.SetActive(false);
		_coreStateMachine.LevelGameStateMachine.SetGameState(GameStateEnum.CountDown);
	}

	private void OnExitToMenuButton()
	{
		// LoadLevel(ScenesStateEnum.Menu);
		_viewMenuUI.gameObject.SetActive(false);
		_coreStateMachine.LevelGameStateMachine.SetGameState(GameStateEnum.None);
		_coreStateMachine.SetScenesState(ScenesStateEnum.Menu);
		PlayClip();
	}

	// private void LoadLevel(ScenesStateEnum scenesStateEnum)
	// {
	//     PlayClip();
	//     _coreStateMachine.SetScenesState(scenesStateEnum);
	//     _multiSceneManager.LoadScene(ScenesStateEnum.Menu);
	//     _coreStateMachine.SceneEndLoad += OnSceneEndLoad;
	// }

	// private void OnSceneEndLoad(ScenesStateEnum scenesStateEnum)
	// {
	//     _coreStateMachine.LevelGameStateMachine.SetGameState(GameStateEnum.None);
	//     _manager.Hide(this);
	//     _manager.Show<MainMenuWindow>();
	// }

	private void PlayClip()
	{
		_audioManager.EventInstances[(int)AudioNameEnum.MenuBackgroundMusic].start();
		_audioManager.EventInstances[(int)AudioNameEnum.GameBackgroundMusic].stop(STOP_MODE.ALLOWFADEOUT);
	}

	private void ShowMenu(GameStateEnum gameStateEnum)
	{
		if (gameStateEnum == GameStateEnum.RaceOver)
		{
			_viewMenuUI.gameObject.SetActive(true);
		}
	}
}