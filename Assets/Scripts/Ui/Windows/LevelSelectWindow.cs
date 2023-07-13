using Enums;
using RoadRage.Controllers;
using RoadRage.StateMachines;
using Tools.UiManager;
using Ui.Common.Tools;
using UnityEngine;
using VContainer;

namespace Ui.Windows
{
    public class LevelSelectWindow : Window
    {
        [SerializeField] private UiButton _backButton;
        [SerializeField] private UiButton _level1Button;
        [SerializeField] private UiButton _level2Button;
        [SerializeField] private UiButton _level3Button;
        [SerializeField] private UiButton _level4Button;
        [SerializeField] private UiButton _level5Button;
        // [SerializeField] private AudioSource _clickClip;

        [Inject] private readonly ICoreStateMachine _coreStateMachine;
        // [Inject] private readonly WindowManager _windowManager;

        protected override void OnActivate()
        {
            base.OnActivate();
            _backButton.OnClick += OnBackButton;
            _level1Button.OnClick += OnLevel1Button;
            _level2Button.OnClick += OnLevel2Button;
            _level3Button.OnClick += OnLevel3Button;
            _level4Button.OnClick += OnLevel4Button;
            _level5Button.OnClick += OnLevel5Button;
        }

        protected override void OnDeactivate()
        {
            base.OnDeactivate();
            _backButton.OnClick -= OnBackButton;
            _level1Button.OnClick -= OnLevel1Button;
            _level2Button.OnClick -= OnLevel2Button;
            _level3Button.OnClick -= OnLevel3Button;
            _level4Button.OnClick -= OnLevel4Button;
            _level5Button.OnClick -= OnLevel5Button;
            _coreStateMachine.SceneEndLoad -= OnSceneEndLoad;
        }

        private void OnBackButton()
        {
            // PlayClip();
            _manager.Hide<LevelSelectWindow>();
            _manager.Show<CarSelectWindow>();
        }

        private void OnLevel1Button()
        {
            _manager.Hide<MainMenuWindow>();
            // _windowManager.gameObject.SetActive(false);
            LoadLevel(ScenesStateEnum.Level1);
        }    
        
        private void OnLevel2Button()
        {
            _manager.Hide<MainMenuWindow>();
            LoadLevel(ScenesStateEnum.Level2);
        }      
        
        private void OnLevel3Button()
        {
            _manager.Hide<MainMenuWindow>();
            LoadLevel(ScenesStateEnum.Level3);
        }      
        
        private void OnLevel4Button()
        {
            _manager.Hide<MainMenuWindow>();
            LoadLevel(ScenesStateEnum.Level4);
        }    
        
        private void OnLevel5Button()
        {
            _manager.Hide<MainMenuWindow>();
            LoadLevel(ScenesStateEnum.Level5);
        }

        private void LoadLevel(ScenesStateEnum scenesStateEnum)
        {
            // PlayClip();
            _coreStateMachine.SceneEndLoad += OnSceneEndLoad;
            _coreStateMachine.SetScenesState(scenesStateEnum);
        }

        private void OnSceneEndLoad(ScenesStateEnum scenesStateEnum)
        {
            _manager.Hide(this);
        }
        // private void PlayClip() => _clickClip.Play();
    }
}