using System;
using DG.Tweening;
using Tools.UiManager;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Common.Tools
{
    public class UiButton : UIBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private DOTweenAnimation _doTweenAnimation;

        public event Action OnClick;

        protected override void OnEnable()
        {
            base.OnEnable();
            _button.OnClickAsObservable().TakeUntilDisable(this).Subscribe(_ => OnButtonClick());
        }

        private void OnButtonClick()
        {
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
}