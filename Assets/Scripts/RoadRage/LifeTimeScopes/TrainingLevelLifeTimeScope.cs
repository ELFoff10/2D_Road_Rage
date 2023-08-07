using VContainer;
using VContainer.Unity;

namespace RoadRage.LifeTimeScopes
{
	public class TrainingLevelLifeTimeScope : LifetimeScope
	{
		protected override void Configure(IContainerBuilder builder)
		{
			builder.RegisterEntryPoint<LevelControllerModel>();
			builder.RegisterComponent(FindObjectOfType<CameraController>());
		}
	}
}