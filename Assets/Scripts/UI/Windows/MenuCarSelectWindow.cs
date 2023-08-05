using Tools.UiManager;
using UnityEngine;

public class MenuCarSelectWindow : Window
{
	[SerializeField]
	private UiButton _backButton;
	[SerializeField]
	private UiButton _selectButton;

	protected override void OnActivate()
	{
		base.OnActivate();
		_backButton.OnClick += OnBackButton;
		_selectButton.OnClick += OnSelectButton;
	}

	protected override void OnDeactivate()
	{
		base.OnDeactivate();
		_backButton.OnClick -= OnBackButton;
		_selectButton.OnClick -= OnSelectButton;
	}

	private void OnBackButton()
	{
		_manager.Hide<MenuCarSelectWindow>();
		_manager.Show<MenuMainWindow>();
	}

	private void OnSelectButton()
	{
		_manager.Hide<MenuCarSelectWindow>();
		_manager.Show<MenuLevelSelectWindow>();
	}
}