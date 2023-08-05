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
		_windowManager.Show<MenuMainWindow>();
		_windowManager.Show<MenuBackgroundWindow>(WindowPriority.Bg);
	}

	private void OnDisable()
	{
		_windowManager.Hide<MenuMainWindow>();
		_windowManager.Hide<MenuBackgroundWindow>();
	}
}