using RoadRage.MultiScene;
using Tools.UiManager;
using UnityEngine;
using VContainer;

namespace RoadRage.Controllers
{
	public class GameInstance : MonoBehaviour
	{
		[Inject]
		private readonly IMultiSceneManager _multiSceneManager;
		[Inject]
		private readonly ICoreStateMachine _coreStateMachine;
		[Inject]
		private readonly IWindowManager _windowManager;
		[Inject]
		private readonly DataCentralService _dataCentralService;

		private void Awake()
		{
			DontDestroyOnLoad(this);
			RegServices();
			SetupFrameTimes();
			// _coreStateMachine.SceneEndLoadFade += OnSceneEndLoad;
		}

		private async void RegServices()
		{
			await _dataCentralService.LoadData();
			//_dataCentralService.SaveFull();
			//_dataCentralService.Restart();

			_coreStateMachine.SetScenesState(ScenesStateEnum.Menu);
		}

		private void SetupFrameTimes() => Application.targetFrameRate = 90;

		private void OnSceneEndLoad(ScenesStateEnum scenesStateEnum)
		{
			// if (scenesStateEnum.Equals(ScenesStateEnum.Game))
			// {
			//     _coreStateMachine.SceneEndLoadFade -= OnSceneEndLoad;
			//     
			//     _iapService.SetGameLoadingFinished();
			// }
		}
	}
}