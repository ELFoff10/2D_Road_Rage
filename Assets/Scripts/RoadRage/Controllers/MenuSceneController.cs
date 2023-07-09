using Enums;
using RoadRage.StateMachines;
using RoadRage.Tools.UiManager;
using Tools.UiManager;
using Ui.Windows;
using UnityEngine;
using VContainer;

namespace RoadRage.Controllers
{
    public class MenuSceneController : MonoBehaviour
    {
        [Inject]
        private readonly ICoreStateMachine _coreStateMachine;

        [Inject]
        private readonly IWindowManager _windowManager;

        private readonly ScenesStateEnum ScenesStateEnum = ScenesStateEnum.Menu;

        private void OnEnable()
        {
            _coreStateMachine.SceneEndLoadFade += OnSceneEndLoadFade;
            _windowManager.Show<MainMenuWindow>();
            _windowManager.Show<BackgroundMenuWindow>(WindowPriority.Bg);
        }

        private void OnDisable()
        {
            _coreStateMachine.SceneEndLoadFade -= OnSceneEndLoadFade;
            _windowManager.Hide<MainMenuWindow>();
            _windowManager.Hide<BackgroundMenuWindow>();
        }

        private void OnSceneEndLoadFade(ScenesStateEnum scenesStateEnum)
        {
            if (scenesStateEnum == ScenesStateEnum)
            {
                EndSceneLoad();
            }
        }

        private void EndSceneLoad()
        {
            _coreStateMachine.SceneEndLoadFade -= OnSceneEndLoadFade;
            //TODO: Что то произойдет, когда пройдет загрузочный Fade 
        }
    }
}