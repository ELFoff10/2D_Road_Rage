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
	private UiButton _resumeButtonTrainingUI;
	[SerializeField]
	private UiButton _raceAgainButton;
	[SerializeField]
	private UiButton _exitButton;	
	[SerializeField]
	private UiButton _exitButtonTraining;
	[SerializeField]
	private LeaderBoardUIHandler _leaderBoardUI;
	[SerializeField]
	private MenuUI _menuUI;
	[SerializeField]
	private TrainingUI _trainingUI;
	[SerializeField]
	private RaceTimeUIHandler _raceTimeUIHandler;
	[SerializeField]
	private CountDownUIHandler _countDownUIHandler;
	[SerializeField]
	private TMP_Text _menuUITextMenu;
	[SerializeField]
	private TMP_Text _menuUITextRaceOver;
	[SerializeField]
	private TMP_Text _trainingTextLeft1;
	[SerializeField]
	private TMP_Text _trainingTextLeft2;	
	[SerializeField]
	private TMP_Text _trainingTextRight1;
	[SerializeField]
	private TMP_Text _trainingTextRight2;	
	[SerializeField]
	private TMP_Text _congratulationText;	
	[SerializeField]
	private TMP_Text _trainingHeaderText;

	// [SerializeField]
	// private DistanceUIHandler _distanceUI;
	// [SerializeField]
	// private RaceTimeUIHandler _raceTimeUI;

	protected override void OnActivate()
	{
		base.OnActivate();

		if (_coreStateMachine.ScenesState.Value == ScenesStateEnum.TrainingLevel)
		{
			_trainingTextLeft1.gameObject.SetActive(true);
			_trainingTextLeft2.gameObject.SetActive(true);
			_trainingTextRight1.gameObject.SetActive(false);
			_trainingTextRight2.gameObject.SetActive(false);
		}

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
		_resumeButtonTrainingUI.OnClick += OnResumeTrainingGame;
		_raceAgainButton.OnClick += OnRaceAgainButton;
		_exitButton.OnClick += OnExitButton;
		_exitButtonTraining.OnClick += OnExitButton;
		_coreStateMachine.LevelGameStateMachine.OnSetGameState += ShowMenu;
	}

	protected override void OnDeactivate()
	{
		base.OnDeactivate();
		_menuButton.OnClick -= OnMenuButton;
		_resumeButton.OnClick -= OnResumeGame;
		_resumeButtonTrainingUI.OnClick -= OnResumeTrainingGame;
		_raceAgainButton.OnClick -= OnRaceAgainButton;
		_exitButton.OnClick -= OnExitButton;
		_exitButtonTraining.OnClick -= OnExitButton;
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
	
	private void OnResumeTrainingGame()
	{
		Time.timeScale = 1;
		_trainingUI.gameObject.SetActive(false);
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
		_trainingUI.gameObject.SetActive(false);
		_coreStateMachine.LevelGameStateMachine.SetGameState(GameStateEnum.None);
		StopClip();
		_audioManager.EventInstances[(int)AudioNameEnum.MenuBackgroundMusic].start();
		LoadLevel(ScenesStateEnum.Menu);
	}
	private void OnExitButtonTraining()
	{
		Time.timeScale = 1;
		_menuButton.gameObject.SetActive(true);
		_menuUI.gameObject.SetActive(false);
		_trainingUI.gameObject.SetActive(false);
		_trainingHeaderText.gameObject.SetActive(false);
		_coreStateMachine.LevelGameStateMachine.SetGameState(GameStateEnum.None);
		StopClip();
		_audioManager.EventInstances[(int)AudioNameEnum.MenuBackgroundMusic].start();
		LoadLevel(ScenesStateEnum.Menu);
	}

	private void ShowMenu(GameStateEnum gameStateEnum)
	{
		switch (gameStateEnum)
		{
			case GameStateEnum.RaceOver:
				Time.timeScale = 0;
				StopClip();
				_leaderBoardUI.Canvas.enabled = true;
				_menuUI.gameObject.SetActive(true);
				_menuUITextRaceOver.gameObject.SetActive(true);
				_menuButton.gameObject.SetActive(false);
				_resumeButton.gameObject.SetActive(false);
				_menuUITextMenu.gameObject.SetActive(false);
				break;
			case GameStateEnum.TrainingCheckPoint1:
				Time.timeScale = 0;
				StopClip();
				_trainingUI.gameObject.SetActive(true);
				_trainingTextRight1.gameObject.SetActive(false);
				_trainingTextRight2.gameObject.SetActive(false);
				_congratulationText.gameObject.SetActive(false);
				_resumeButtonTrainingUI.gameObject.SetActive(true);
				_trainingHeaderText.gameObject.SetActive(true);
				_trainingTextLeft1.gameObject.SetActive(true);
				_trainingTextLeft2.gameObject.SetActive(true);
				break;		
			case GameStateEnum.TrainingCheckPoint2:
				Time.timeScale = 0;
				StopClip();
				_trainingUI.gameObject.SetActive(true);
				_trainingTextLeft1.gameObject.SetActive(false);
				_trainingTextLeft2.gameObject.SetActive(false);
				_congratulationText.gameObject.SetActive(false);
				_trainingHeaderText.gameObject.SetActive(true);
				_trainingTextRight1.gameObject.SetActive(true);
				_trainingTextRight2.gameObject.SetActive(true);
				break;
			case GameStateEnum.TrainingCheckPoint3:
				Time.timeScale = 0;
				StopClip();
				_trainingUI.gameObject.SetActive(true);
				_trainingHeaderText.gameObject.SetActive(false);
				_trainingTextLeft1.gameObject.SetActive(false);
				_trainingTextLeft2.gameObject.SetActive(false);
				_resumeButtonTrainingUI.gameObject.SetActive(false);
				_trainingTextRight1.gameObject.SetActive(false);
				_trainingTextRight2.gameObject.SetActive(false);
				_congratulationText.gameObject.SetActive(true);
				break;
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