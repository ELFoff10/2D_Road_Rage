using RoadRage.MultiScene;
using Tools.UiManager;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class MainLifeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent(Object.FindObjectOfType<WindowManager>()).As<IWindowManager>();
        builder.RegisterComponent(Object.FindObjectOfType<AudioManager>());
        builder.RegisterComponent(Object.FindObjectOfType<FMOD_Events>());
        builder.RegisterEntryPoint<ScenesControllerModel>();
        builder.Register<PrefabInject>(Lifetime.Singleton);
        builder.Register<IMultiSceneManager, MultiSceneManager>(Lifetime.Singleton);
        builder.Register<ICoreStateMachine, CoreStateMachine>(Lifetime.Singleton);
        builder.Register<TimerService>(Lifetime.Singleton).As<ITimerService>();
        builder.Register<DataCentralService>(Lifetime.Singleton).As<IDataCentralService, DataCentralService>();
    }
}