using Enums;
using FMOD.Studio;
using RoadRage.MultiScene;
using TMPro;
using Tools.UiManager;
using UnityEngine;
using VContainer;

public class GameTrainingLevelWindow : Window
{
	[Inject]
	private readonly ICoreStateMachine _coreStateMachine;
	[Inject]
	private readonly AudioManager _audioManager;
	[Inject]
	private readonly IMultiSceneManager _multiSceneManager;
	[SerializeField]
	private UiButton _resumeButtonTrainingUI;
	[SerializeField]
	private UiButton _exitButtonTraining;
	[SerializeField]
	private TrainingUI _trainingUI;
	[SerializeField]
	private CountDownUIHandler _countDownUIHandler;
	[SerializeField]
	private TMP_Text _trainingTextLeft1;
	[SerializeField]
	private TMP_Text _trainingTextLeft2;
	[SerializeField]
	private TMP_Text _trainingTextRight1;
	[SerializeField]
	private TMP_Text _trainingTextRight2;
	[SerializeField]
	private GameObject _congratulationPanel;
	[SerializeField]
	private TMP_Text _trainingHeaderText;

	protected override void OnActivate()
	{
		base.OnActivate();

		_countDownUIHandler.gameObject.SetActive(true);
		_trainingTextLeft1.gameObject.SetActive(true);
		_trainingTextLeft2.gameObject.SetActive(true);
		_trainingTextRight1.gameObject.SetActive(false);
		_trainingTextRight2.gameObject.SetActive(false);

		_resumeButtonTrainingUI.OnClick += OnResumeTrainingGame;
		_exitButtonTraining.OnClick += OnExitButton;
		_coreStateMachine.LevelGameStateMachine.OnSetGameState += ShowMenu;
	}

	protected override void OnDeactivate()
	{
		base.OnDeactivate();
		_resumeButtonTrainingUI.OnClick -= OnResumeTrainingGame;
		_exitButtonTraining.OnClick -= OnExitButton;
		_coreStateMachine.LevelGameStateMachine.OnSetGameState -= ShowMenu;
		_coreStateMachine.SceneEndLoad -= OnSceneEndLoad;
	}

	private void OnResumeTrainingGame()
	{
		Time.timeScale = 1;
		_trainingUI.gameObject.SetActive(false);
		_coreStateMachine.LevelGameStateMachine.SetGameState(GameStateEnum.Play);
		_audioManager.EventInstances[(int)AudioNameEnum.CarEngine].start();
		_audioManager.EventInstances[(int)AudioNameEnum.CarSkid].start();
	}

	private void OnExitButton()
	{
		Time.timeScale = 1;
		_trainingUI.gameObject.SetActive(false);
		StopClip();
		_audioManager.EventInstances[(int)AudioNameEnum.MenuBackgroundMusic].start();
		LoadLevel(ScenesStateEnum.Menu);
	}

	private void ShowMenu(GameStateEnum gameStateEnum)
	{
		switch (gameStateEnum)
		{
			// case GameStateEnum.RaceOver:
			// 	Time.timeScale = 0;
			// 	StopClip();
			// 	break;
			case GameStateEnum.TrainingCheckPoint1:
				Time.timeScale = 0;
				StopClip();
				_trainingUI.gameObject.SetActive(true);
				_trainingTextRight1.gameObject.SetActive(false);
				_trainingTextRight2.gameObject.SetActive(false);
				_congratulationPanel.gameObject.SetActive(false);
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
				_congratulationPanel.gameObject.SetActive(false);
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
				_congratulationPanel.gameObject.SetActive(true);
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
		_audioManager.EventInstances[(int)AudioNameEnum.CarHit].stop(STOP_MODE.IMMEDIATE);
		
	}
}