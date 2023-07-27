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
	private LeaderBoardUIHandler _leaderBoardUI;	
	[SerializeField]
	private MenuUI _menuUI;
	[SerializeField]
	private CountDownUIHandler _countDownUIHandler;
	
	// [SerializeField]
	// private DistanceUIHandler _distanceUI;
	// [SerializeField]
	// private RaceTimeUIHandler _raceTimeUI;

	protected override void OnActivate()
	{
		base.OnActivate();
		
		// if (_coreStateMachine.ScenesState.Value == ScenesStateEnum.Level4)
		// {
		// 	_raceTimeUI.gameObject.SetActive(false);
		// 	_distanceUI.gameObject.SetActive(true);
		// }
		// else
		// {
		// 	_distanceUI.gameObject.SetActive(false);
		// }

		_menuButton.OnClick += OnMenuButton;
		_raceAgainButton.OnClick += OnRaceAgainButton;
		_exitToMenuButton.OnClick += OnExitToMenuButton;
		_coreStateMachine.LevelGameStateMachine.OnSetGameState += ShowMenu;
	}

	protected override void OnDeactivate()
	{
		base.OnDeactivate();
		_menuUI.gameObject.SetActive(false);
		_menuButton.OnClick -= OnMenuButton;
		_raceAgainButton.OnClick -= OnRaceAgainButton;
		_exitToMenuButton.OnClick -= OnExitToMenuButton;
		_coreStateMachine.LevelGameStateMachine.OnSetGameState -= ShowMenu;
		// _coreStateMachine.SceneEndLoad -= OnSceneEndLoad;
	}

	private void OnMenuButton()
	{
		_menuUI.gameObject.SetActive(true);
		_countDownUIHandler.gameObject.SetActive(true);
		_coreStateMachine.LevelGameStateMachine.SetGameState(GameStateEnum.PrePlay);
		// _audioManager.EventInstances[(int)AudioNameEnum.GameBackgroundMusic].stop(STOP_MODE.ALLOWFADEOUT);
		// _audioManager.EventInstances[(int)AudioNameEnum.CarEngine].stop(STOP_MODE.IMMEDIATE);
	}

	private void OnRaceAgainButton()
	{
		_menuUI.gameObject.SetActive(false);
		_leaderBoardUI.gameObject.SetActive(false);
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		_coreStateMachine.LevelGameStateMachine.SetGameState(GameStateEnum.CountDown);
		_countDownUIHandler.gameObject.SetActive(true);
	}

	private void OnExitToMenuButton()
	{
		// LoadLevel(ScenesStateEnum.Menu);
		_menuUI.gameObject.SetActive(false);
		_audioManager.EventInstances[(int)AudioNameEnum.GameBackgroundMusic].stop(STOP_MODE.ALLOWFADEOUT);
		_audioManager.EventInstances[(int)AudioNameEnum.CarEngine].stop(STOP_MODE.IMMEDIATE);
		_audioManager.EventInstances[(int)AudioNameEnum.CarSkid].stop(STOP_MODE.IMMEDIATE);
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
			_menuUI.gameObject.SetActive(true);
			_leaderBoardUI.Canvas.enabled = true;
		}
	}
}