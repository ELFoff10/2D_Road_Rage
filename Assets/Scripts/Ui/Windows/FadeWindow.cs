using System;
using DG.Tweening;
using RoadRage.Tools.GameHelpers;
using Tools.UiManager;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Ui.Windows
{
    public class FadeWindow : Window
    {
        [SerializeField] private DOTweenAnimation _doTweenAnimation;

        private Action _fadeSceneDelegate;
        protected override void OnDeactivate()
        {
            base.OnDeactivate();
            _fadeSceneDelegate = null;
        }

        public void OpenFade(Action endBack = null)
        {
            _fadeSceneDelegate = endBack;
            _doTweenAnimation.DORestartById(StringsStatic.Helper.Open);
            _doTweenAnimation.DOPlayById(StringsStatic.Helper.Open);
        }

        public void CloseFade(Action endBack = null)
        {
            _fadeSceneDelegate = endBack;
            _doTweenAnimation.DORestartById(StringsStatic.Helper.Close);
            _doTweenAnimation.DOPlayById(StringsStatic.Helper.Close);
        }
        
        public void EndOpenFade()
        {
            _fadeSceneDelegate?.Invoke();
            Debug.Log("Open scene Fade ended");
            _manager.Hide(this);
        }
        
        public void EndCloseFade()
        {
            _fadeSceneDelegate?.Invoke();
            Debug.Log("Close scene Fade ended");
        }
    }
}