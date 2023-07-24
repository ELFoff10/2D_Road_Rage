using RoadRage.MultiScene;
using Tools.UiManager;
using Ui.Windows;
using UniRx;
using VContainer;
using VContainer.Unity;

public class ScenesControllerModel : IInitializable
{
	private ScenesStateEnum _scene = ScenesStateEnum.Base;
	[Inject]
	private readonly IMultiSceneManager _multiSceneManager;
	[Inject]
	private readonly ICoreStateMachine _coreStateMachine;
	[Inject]
	private readonly IWindowManager _windowManager;

	public void Initialize()
	{
		_coreStateMachine.ScenesState.SkipLatestValueOnSubscribe().Subscribe(CurrentSceneSwitches);
	}

	private void CurrentSceneSwitches(ScenesStateEnum scene)
	{
		_windowManager.Show<FadeWindow>(WindowPriority.LoadScene).CloseFade(EndCloseFade);
		_scene = scene;
	}

	private void EndCloseFade()
	{
		_coreStateMachine.OnSceneEndLoad();
		_multiSceneManager.LoadScene(_scene, NextSceneEndLoad);
	}

	private void EndOpenFade() => _coreStateMachine.OnSceneEndLoadFade();

	private void NextSceneEndLoad()
	{
		_multiSceneManager.UnloadLastScene();
		_multiSceneManager.SetActiveLastLoadScene();
		_windowManager.Show<FadeWindow>().OpenFade(EndOpenFade);
	}
}