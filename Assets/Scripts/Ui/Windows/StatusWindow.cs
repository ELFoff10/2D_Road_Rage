using Tools.UiManager;
using UnityEngine;

public class StatusWindow : Window
{
	[SerializeField]
	private UiButton _backButton;

	protected override void OnActivate()
	{
		base.OnActivate();
		_backButton.OnClick += OnBackButton;
	}

	protected override void OnDeactivate()
	{
		base.OnDeactivate();
		_backButton.OnClick -= OnBackButton;
	}

	private void OnBackButton()
	{
		_manager.Hide<StatusWindow>();
		_manager.Show<MainMenuWindow>();
	}
}