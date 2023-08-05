using RoadRage.MultiScene;
using Tools.UiManager;
using VContainer;
using VContainer.Unity;

public class MainLifeScope : LifetimeScope
{
	protected override void Configure(IContainerBuilder builder)
	{
		builder.RegisterComponent(FindObjectOfType<WindowManager>()).As<IWindowManager>();
		builder.RegisterComponent(FindObjectOfType<AudioManager>());
		builder.RegisterComponent(FindObjectOfType<FMOD_Events>());
		builder.RegisterEntryPoint<ScenesControllerModel>();
		builder.Register<PrefabInject>(Lifetime.Singleton);
		builder.Register<IMultiSceneManager, MultiSceneManager>(Lifetime.Singleton);
		builder.Register<ICoreStateMachine, CoreStateMachine>(Lifetime.Singleton);
		builder.Register<TimerService>(Lifetime.Singleton).As<ITimerService>();
		// builder.Register<DataCentralService>(Lifetime.Singleton).As<IDataCentralService, DataCentralService>();
	}
}