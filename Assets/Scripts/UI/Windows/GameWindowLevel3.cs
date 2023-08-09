using FMOD.Studio;
using RoadRage.MultiScene;
using TMPro;
using Tools.UiManager;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

public class GameWindowLevel3 : Window
{
	[SerializeField]
	private UiButton _menuButton;
	[SerializeField]
	private UiButton _resumeButton;
	[SerializeField]
	private UiButton _raceAgainButton;
	[SerializeField]
	private UiButton _exitButton;
	[SerializeField]
	private MenuUI _menuUI;
	[SerializeField]
	private LifeCountUI _lifeCountUI;
	[SerializeField]
	private GemCountUI _gemCountUI;
	[SerializeField]
	private CountDownUIHandler _countDownUIHandler;
	[SerializeField]
	private DistanceUIHandler _distanceUIHandler;
	[SerializeField]
	private TMP_Text _menuUITextMenu;
	[SerializeField]
	private TMP_Text _menuUITextRaceOver;
	[SerializeField]
	private TMP_Text _menuUITextFinish;
	[Inject]
	private readonly ICoreStateMachine _coreStateMachine;
	[Inject]
	private readonly AudioManager _audioManager;
	[Inject]
	private readonly IMultiSceneManager _multiSceneManager;

	protected override void OnActivate()
	{
		base.OnActivate();
		_countDownUIHandler.gameObject.SetActive(true);
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
		_menuUITextFinish.gameObject.SetActive(false);
		_menuUITextMenu.gameObject.SetActive(true);
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
		_gemCountUI.TotalGems = 0;
		_gemCountUI.UpdateText();
		_lifeCountUI.LifeCount = 3;
		_lifeCountUI.UpdateText();
		
		_menuButton.gameObject.SetActive(true);
		_menuUI.gameObject.SetActive(false);
		_countDownUIHandler.gameObject.SetActive(true);
		_lifeCountUI.gameObject.SetActive(false);
		_lifeCountUI.gameObject.SetActive(true);
		PlayClip();
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		_coreStateMachine.LevelGameStateMachine.SetGameState(GameStateEnum.CountDown);
	}

	private void OnExitButton()
	{
		Time.timeScale = 1;
		_gemCountUI.TotalGems = 0;
		_menuButton.gameObject.SetActive(true);
		_menuUI.gameObject.SetActive(false);
		StopClip();
		_audioManager.EventInstances[(int)AudioNameEnum.MenuBackgroundMusic].start();
		LoadLevel(ScenesStateEnum.Menu);
		_coreStateMachine.LevelGameStateMachine.SetGameState(GameStateEnum.CountDown);
	}

	private void ShowMenu(GameStateEnum gameStateEnum)
	{
		switch (gameStateEnum)
		{
			case GameStateEnum.Finish:
				Time.timeScale = 0;
				StopClip();
				_menuUI.gameObject.SetActive(true);
				_menuUITextMenu.gameObject.SetActive(false);
				_menuUITextRaceOver.gameObject.SetActive(false);
				_menuUITextFinish.gameObject.SetActive(true);
				_menuButton.gameObject.SetActive(false);
				_resumeButton.gameObject.SetActive(false);
				_menuUITextMenu.gameObject.SetActive(false);
				break;
			case GameStateEnum.Dead:
				Time.timeScale = 0;
				StopClip();
				_menuButton.gameObject.SetActive(false);
				_resumeButton.gameObject.SetActive(false);
				_menuUITextMenu.gameObject.SetActive(false);
				_menuUITextFinish.gameObject.SetActive(false);
				_menuUITextRaceOver.gameObject.SetActive(true);
				_menuUI.gameObject.SetActive(true);
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
		_manager.Show<MenuMainWindow>();
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