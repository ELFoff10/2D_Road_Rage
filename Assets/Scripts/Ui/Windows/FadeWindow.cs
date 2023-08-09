using System;
using System.Collections.Generic;
using DG.Tweening;
using Tools.UiManager;
using UnityEngine;

public class FadeWindow : Window
{
	[SerializeField]
	private List<DOTweenAnimation> _doTweenAnimations;

	private Action _fadeSceneDelegate;

	protected override void OnDeactivate()
	{
		base.OnDeactivate();
		_fadeSceneDelegate = null;
	}

	public void OpenFade(Action endBack = null)
	{
		_fadeSceneDelegate = endBack;
		foreach (var doTweenAnimation in _doTweenAnimations)
		{
			doTweenAnimation.DORestartById(StringsStatic.Helper.Open);
			doTweenAnimation.DOPlayById(StringsStatic.Helper.Open);
		}
	}

	public void CloseFade(Action endBack = null)
	{
		_fadeSceneDelegate = endBack;
		foreach (var doTweenAnimation in _doTweenAnimations)
		{
			doTweenAnimation.DORestartById(StringsStatic.Helper.Close);
			doTweenAnimation.DOPlayById(StringsStatic.Helper.Close);
		}
	}

	public void EndOpenFade()
	{
		_fadeSceneDelegate?.Invoke();
		_manager.Hide(this);
	}

	public void EndCloseFade()
	{
		_fadeSceneDelegate?.Invoke();
	}
}