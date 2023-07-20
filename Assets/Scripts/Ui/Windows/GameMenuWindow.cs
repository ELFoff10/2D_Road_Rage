using Enums;
using FMOD.Studio;
using Tools.UiManager;
using Ui.Common.Tools;
using UnityEngine;
using VContainer;

namespace Ui.Windows
{
    public class GameMenuWindow : Window
    {
        [Inject] 
        private readonly ICoreStateMachine _coreStateMachine;
        
        [Inject] 
        private readonly AudioManager _audioManager;
        
        [SerializeField] private UiButton _menuButton;
        [SerializeField] private UiButton _exitToMenuButton;
        [SerializeField] private GameObject _menuCanvas;

        protected override void OnActivate()
        {
            base.OnActivate();
            _menuButton.OnClick += OnMenuButton;
        }

        protected override void OnDeactivate()
        {
            base.OnDeactivate();
            _menuButton.OnClick -= OnMenuButton;
            // _coreStateMachine.SceneEndLoad -= OnSceneEndLoad;
        }
        
        // private void OnSceneEndLoad(ScenesStateEnum scenesStateEnum)
        // {
        //     _manager.Hide(this);
        // }
        
        private void OnMenuButton()
        {            
            _menuCanvas.gameObject.SetActive(true);
            _audioManager.EventInstances[(int)AudioNameEnum.GameBackgroundMusic].stop(STOP_MODE.ALLOWFADEOUT);
            _audioManager.EventInstances[(int)AudioNameEnum.CarEngine].stop(STOP_MODE.IMMEDIATE);
            Time.timeScale = 0;
        }
        
        private void OnExitToMenuButton()
        {
            LoadLevel(ScenesStateEnum.Level1);
            _coreStateMachine.SetScenesState(ScenesStateEnum.Menu);
            _manager.Show<MainMenuWindow>();
            _audioManager.EventInstances[(int)AudioNameEnum.GameBackgroundMusic].stop(STOP_MODE.ALLOWFADEOUT);
            Time.timeScale = 0;
        }
        
        private void LoadLevel(ScenesStateEnum scenesStateEnum)
        {
            PlayClip();
            _coreStateMachine.SceneEndLoad += OnSceneEndLoad;
            _coreStateMachine.SetScenesState(scenesStateEnum);
        }

        private void OnSceneEndLoad(ScenesStateEnum scenesStateEnum)
        {
            _manager.Hide(this);
            _manager.Show<MainMenuWindow>();
            _coreStateMachine.LevelGameStateMachine.SetGameState(GameStateEnum.None);
            _coreStateMachine.SetScenesState(ScenesStateEnum.Base);
        }

        private void PlayClip()
        {
            _audioManager.EventInstances[(int)AudioNameEnum.MenuBackgroundMusic].start();
            _audioManager.EventInstances[(int)AudioNameEnum.GameBackgroundMusic].stop(STOP_MODE.ALLOWFADEOUT);
        }
    }
}