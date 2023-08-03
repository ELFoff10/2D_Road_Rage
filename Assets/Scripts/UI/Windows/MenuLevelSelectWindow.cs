using Enums;
using Tools.UiManager;
using UnityEngine;
using VContainer;

public class MenuLevelSelectWindow : Window
{
	[SerializeField]
	private UiButton _backButton;
	[SerializeField]
	private UiButton _trainingLevelButton, _level1Button, _level2Button, _level3Button, _level4Button, _level5Button;
	[Inject]
	private readonly ICoreStateMachine _coreStateMachine;
	[Inject]
	private readonly AudioManager _audioManager;
	[Inject]
	private readonly FMOD_Events _fmodEvents;

	protected override void OnActivate()
	{
		base.OnActivate();
		_backButton.OnClick += OnBackButton;
		_trainingLevelButton.OnClick += OnTrainingLevelButton;
		_level1Button.OnClick += OnLevel1Button;
		_level2Button.OnClick += OnLevel2Button;
		_level3Button.OnClick += OnLevel3Button;
		_level4Button.OnClick += OnLevel4Button;
		_level5Button.OnClick += OnLevel5Button;
	}

	protected override void OnDeactivate()
	{
		base.OnDeactivate();
		_backButton.OnClick -= OnBackButton;
		_trainingLevelButton.OnClick -= OnTrainingLevelButton;
		_level1Button.OnClick -= OnLevel1Button;
		_level2Button.OnClick -= OnLevel2Button;
		_level3Button.OnClick -= OnLevel3Button;
		_level4Button.OnClick -= OnLevel4Button;
		_level5Button.OnClick -= OnLevel5Button;
		_coreStateMachine.SceneEndLoad -= OnSceneEndLoad;
	}

	private void OnBackButton()
	{
		_manager.Hide<MenuLevelSelectWindow>();
		_manager.Show<MenuCarSelectWindow>();
	}
	
	private void OnTrainingLevelButton()
	{
		LoadLevel(ScenesStateEnum.TrainingLevel);
	}
	
	private void OnLevel1Button()
	{
		LoadLevel(ScenesStateEnum.Level1);
	}

	private void OnLevel2Button()
	{
		LoadLevel(ScenesStateEnum.Level2);
	}
	
	private void OnLevel3Button()
	{
		LoadLevel(ScenesStateEnum.Level3);
	}

	private void OnLevel4Button()
	{
		LoadLevel(ScenesStateEnum.Level4);
	}

	private void OnLevel5Button()
	{
		LoadLevel(ScenesStateEnum.Level5);
	}

	private void LoadLevel(ScenesStateEnum scenesStateEnum)
	{
		PlayClip();
		_coreStateMachine.SceneEndLoad += OnSceneEndLoad;
		_coreStateMachine.SetScenesState(scenesStateEnum);
	}

	private void OnSceneEndLoad(ScenesStateEnum scenesStateEnum)
	{
		_manager.Hide(this);
		_coreStateMachine.LevelGameStateMachine.SetGameState(GameStateEnum.CountDown);
	}

	private void PlayClip()
	{
		_audioManager.EventInstances[(int)AudioNameEnum.MenuBackgroundMusic].stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
		_audioManager.EventInstances[(int)AudioNameEnum.GameBackgroundMusic].start();
	}
}