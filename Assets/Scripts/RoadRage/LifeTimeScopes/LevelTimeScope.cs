using RoadRage.Controllers;
using VContainer;
using VContainer.Unity;

namespace RoadRage.LifeTimeScopes
{
    public class LevelTimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<LevelControllerModel>();
        }
    }
}