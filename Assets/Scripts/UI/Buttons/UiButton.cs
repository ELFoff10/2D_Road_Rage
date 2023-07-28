using System;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using UIBehaviour = Tools.UiManager.UIBehaviour;

public class UiButton : UIBehaviour
{
	[Inject]
	private readonly AudioManager _audioManager;
	[Inject]
	private readonly FMOD_Events _fmodEvents;

	[SerializeField]
	private Button _button;
	[SerializeField]
	private DOTweenAnimation _doTweenAnimation;

	public event Action OnClick;

	protected override void OnEnable()
	{
		base.OnEnable();
		_doTweenAnimation.isIndependentUpdate = true;
		_button.OnClickAsObservable().TakeUntilDisable(this).Subscribe(_ => OnButtonClick());
	}

	private void OnButtonClick()
	{
		_audioManager.PlayOneShot(_fmodEvents.ClickButton);
		_doTweenAnimation.DORestart();
		_doTweenAnimation.DOPlay();
		_button.interactable = false;
	}

	public void OnCompleteAnimation()
	{
		OnClick?.Invoke();
		_button.interactable = true;
	}
}