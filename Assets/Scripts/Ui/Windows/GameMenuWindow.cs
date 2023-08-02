using Enums;
using FMOD.Studio;
using RoadRage.MultiScene;
using TMPro;
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
	private UiButton _resumeButton;
	[SerializeField]
	private UiButton _raceAgainButton;
	[SerializeField]
	private UiButton _exitButton;
	[SerializeField]
	private LeaderBoardUIHandler _leaderBoardUI;
	[SerializeField]
	private MenuUI _menuUI;
	[SerializeField]
	private RaceTimeUIHandler _raceTimeUIHandler;
	[SerializeField]
	private CountDownUIHandler _countDownUIHandler;
	[SerializeField]
	private TMP_Text _menuUITextMenu;
	[SerializeField]
	private TMP_Text _menuUITextRaceOver;

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
		_resumeButton.OnClick += OnResumeGame;
		_raceAgainButton.OnClick += OnRaceAgainButton;
		_exitButton.OnClick += OnExitButton;
		_coreStateMachine.LevelGameStateMachine.OnSetGameState += ShowMenu;
	}

	protected override void OnDeactivate()
	{
		base.OnDeactivate();
		_menuButton.OnClick -= OnMenuButton;
		_resumeButton.OnClick -= OnResumeGame;
		_raceAgainButton.OnClick -= OnRaceAgainButton;
		_exitButton.OnClick -= OnExitButton;
		_coreStateMachine.LevelGameStateMachine.OnSetGameState -= ShowMenu;
		_coreStateMachine.SceneEndLoad -= OnSceneEndLoad;
	}

	private void OnMenuButton()
	{
		Time.timeScale = 0;
		_menuUITextRaceOver.gameObject.SetActive(false);
		_menuUI.gameObject.SetActive(true);
		_resumeButton.gameObject.SetActive(true);
		_menuUITextMenu.gameObject.SetActive(true);
		_countDownUIHandler.gameObject.SetActive(true);
		_audioManager.EventInstances[(int)AudioNameEnum.CarEngine].stop(STOP_MODE.IMMEDIATE);
		_audioManager.EventInstances[(int)AudioNameEnum.CarSkid].stop(STOP_MODE.IMMEDIATE);
	}

	private void OnResumeGame()
	{
		Time.timeScale = 1;
		_menuUI.gameObject.SetActive(false);
		_countDownUIHandler.gameObject.SetActive(false);
		_coreStateMachine.LevelGameStateMachine.SetGameState(GameStateEnum.Play);
		_audioManager.EventInstances[(int)AudioNameEnum.CarEngine].start();
		_audioManager.EventInstances[(int)AudioNameEnum.CarSkid].start();
	}

	private void OnRaceAgainButton()
	{
		Time.timeScale = 1;
		_menuButton.gameObject.SetActive(true);
		_raceTimeUIHandler.RaceTimer = 0;
		_menuUI.gameObject.SetActive(false);
		_leaderBoardUI.gameObject.SetActive(false);
		_coreStateMachine.LevelGameStateMachine.SetGameState(GameStateEnum.CountDown);
		_countDownUIHandler.gameObject.SetActive(true);
		PlayClip();
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	private void OnExitButton()
	{
		Time.timeScale = 1;
		_menuButton.gameObject.SetActive(true);
		_menuUI.gameObject.SetActive(false);
		_coreStateMachine.LevelGameStateMachine.SetGameState(GameStateEnum.None);
		StopClip();
		_audioManager.EventInstances[(int)AudioNameEnum.MenuBackgroundMusic].start();
		LoadLevel(ScenesStateEnum.Menu);
	}

	private void ShowMenu(GameStateEnum gameStateEnum)
	{
		if (gameStateEnum == GameStateEnum.RaceOver)
		{
			Time.timeScale = 0;
			StopClip();
			_leaderBoardUI.Canvas.enabled = true;
			_menuUI.gameObject.SetActive(true);
			_menuUITextRaceOver.gameObject.SetActive(true);
			_menuButton.gameObject.SetActive(false);
			_resumeButton.gameObject.SetActive(false);
			_menuUITextMenu.gameObject.SetActive(false);
		}
	}

	private void LoadLevel(ScenesStateEnum scenesStateEnum)
	{
		_coreStateMachine.SetScenesState(scenesStateEnum);
		_coreStateMachine.SceneEndLoad += OnSceneEndLoad;
	}

	private void OnSceneEndLoad(ScenesStateEnum scenesStateEnum)
	{
		_coreStateMachine.LevelGameStateMachine.SetGameState(GameStateEnum.CountDown);
		_manager.Hide(this);
		_manager.Show<MainMenuWindow>();
	}

	private void PlayClip()
	{
		_audioManager.EventInstances[(int)AudioNameEnum.GameBackgroundMusic].start();
		_audioManager.EventInstances[(int)AudioNameEnum.CarEngine].start();
		_audioManager.EventInstances[(int)AudioNameEnum.CarSkid].start();
	}

	private void StopClip()
	{
		_audioManager.EventInstances[(int)AudioNameEnum.GameBackgroundMusic].stop(STOP_MODE.ALLOWFADEOUT);
		_audioManager.EventInstances[(int)AudioNameEnum.CarEngine].stop(STOP_MODE.IMMEDIATE);
		_audioManager.EventInstances[(int)AudioNameEnum.CarSkid].stop(STOP_MODE.IMMEDIATE);
	}
}