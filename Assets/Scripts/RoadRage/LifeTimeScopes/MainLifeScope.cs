using DataModels;
using Models.Controllers;
using Models.Timers;
using RoadRage.Controllers;
using RoadRage.MultiScene;
using RoadRage.StateMachines;
using Tools.UiManager;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace RoadRage.LifeTimeScopes
{
    public class MainLifeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(Object.FindObjectOfType<WindowManager>()).As<IWindowManager>();
            builder.RegisterEntryPoint<ScenesControllerModel>();
            builder.Register<UiFabric>(Lifetime.Singleton);
            builder.Register<IMultiSceneManager, MultiSceneManager>(Lifetime.Singleton);
            builder.Register<ICoreStateMachine, CoreStateMachine>(Lifetime.Singleton);
            builder.Register<TimerService>(Lifetime.Singleton).As<ITimerService>();
            builder.Register<DataCentralService>(Lifetime.Singleton).As<IDataCentralService, DataCentralService>();
            builder.Register<CarController>(Lifetime.Singleton);
        }
    }
}