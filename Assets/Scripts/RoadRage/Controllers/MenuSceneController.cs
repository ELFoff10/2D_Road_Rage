using Tools.UiManager;
using UnityEngine;
using VContainer;


public class MenuSceneController : MonoBehaviour
{
	[Inject]
	private readonly ICoreStateMachine _coreStateMachine;
	[Inject]
	private readonly IWindowManager _windowManager;

	private void OnEnable()
	{
		_coreStateMachine.SceneEndLoadFade += OnSceneEndLoadFade;
		_windowManager.Show<MainMenuWindow>();
		_windowManager.Show<BackgroundMenuWindow>(WindowPriority.Bg);
	}

	private void OnDisable()
	{
		_coreStateMachine.SceneEndLoadFade -= OnSceneEndLoadFade;
		_windowManager.Hide<MainMenuWindow>();
		_windowManager.Hide<BackgroundMenuWindow>();
	}

	private void OnSceneEndLoadFade(ScenesStateEnum scenesStateEnum)
	{
		if (scenesStateEnum == ScenesStateEnum.Menu)
		{
			EndSceneLoad();
		}
	}

	private void EndSceneLoad()
	{
		_coreStateMachine.SceneEndLoadFade -= OnSceneEndLoadFade;
		//TODO: What will happen when the boot Fade passes
	}
}