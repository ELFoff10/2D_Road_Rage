using System;
using RoadRage.MultiScene;
using Tools.UiManager;
using UnityEngine;
using VContainer;

public class GameInstance : MonoBehaviour
{
	[Inject]
	private readonly IMultiSceneManager _multiSceneManager;
	[Inject]
	private readonly ICoreStateMachine _coreStateMachine;
	[Inject]
	private readonly IWindowManager _windowManager;
	[Inject]
	private readonly DataCentralService _dataCentralService;
	[Inject]
	private readonly AudioManager _audioManager;

	private void Awake()
	{
		// Time.timeScale = 10f;
		DontDestroyOnLoad(this);
		RegServices();
		SetupFrameTimes();
	}

	private void Start()
	{
		_audioManager.EventInstances[(int)AudioNameEnum.MenuBackgroundMusic].start();
	}

	private async void RegServices()
	{
		await _dataCentralService.LoadData();
		_dataCentralService.SaveFull();
		//_dataCentralService.Restart();

		_coreStateMachine.SetScenesState(ScenesStateEnum.Menu);
	}

	private static void SetupFrameTimes() => Application.targetFrameRate = 90;
}