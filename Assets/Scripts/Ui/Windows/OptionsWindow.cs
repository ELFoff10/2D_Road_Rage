using Tools.UiManager;
using Ui.Common.Tools;
using UnityEngine;

namespace Ui.Windows
{
    public class OptionsWindow : Window
    {
        [SerializeField] private UiButton _backButton;

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
            _manager.Hide<OptionsWindow>();
            _manager.Show<MainMenuWindow>();
        }
    }
}