using Tools.UiManager;
using UnityEngine;


public class MenuCarSelectWindow : Window
{
	[SerializeField]
	private UiButton _backButton;
	[SerializeField]
	private UiButton _selectButton;
	[SerializeField]
	private UiButton _leftButton;
	[SerializeField]
	private UiButton _rightButton;

	protected override void OnActivate()
	{
		base.OnActivate();
		_backButton.OnClick += OnBackButton;
		_selectButton.OnClick += OnSelectButton;
		_leftButton.OnClick += OnLeftButton;
		_rightButton.OnClick += OnRightButton;
	}

	protected override void OnDeactivate()
	{
		base.OnDeactivate();
		_backButton.OnClick -= OnBackButton;
		_selectButton.OnClick -= OnSelectButton;
		_leftButton.OnClick -= OnLeftButton;
		_rightButton.OnClick -= OnRightButton;
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

	private void OnLeftButton()
	{
	}

	private void OnRightButton()
	{
	}
}