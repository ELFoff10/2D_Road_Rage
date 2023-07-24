using VContainer;
using VContainer.Unity;

namespace RoadRage.LifeTimeScopes
{
    public class LevelLifeTimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<LevelControllerModel>();
        }
    }
}