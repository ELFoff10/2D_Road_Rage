using Tools.UiManager;
using Ui.Common.Tools;
using UnityEngine;

namespace Ui.Windows
{
    public class CarSelectWindow : Window
    {
        [SerializeField] private UiButton _backButton;
        [SerializeField] private UiButton _selectButton;
        [SerializeField] private UiButton _leftButton;
        [SerializeField] private UiButton _rightButton;
        // [SerializeField] private AudioSource _clickClip;

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
            // PlayClip();
            _manager.Hide<CarSelectWindow>();
            _manager.Show<MainMenuWindow>();
        }
        
        private void OnSelectButton()
        {
            // PlayClip();
            _manager.Hide<CarSelectWindow>();
            _manager.Show<LevelSelectWindow>();
        }
        
        private void OnLeftButton()
        {
            // PlayClip();
        }
        
        private void OnRightButton()
        {
            // PlayClip();
        }

        // private void PlayClip() => _clickClip.Play();
    }
}