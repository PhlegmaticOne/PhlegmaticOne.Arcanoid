using Common.Scenes;
using Libs.Behaviors;
using Libs.Behaviors.Installer;
using Libs.Services;

namespace Game.GameEntities.PlayerObjects.BallObject.Behaviors.Bottom
{
    public class RemoveFromBallsOnFieldBehaviorInstaller : BehaviorInstaller<Ball>
    {
        public override IObjectBehavior<Ball> CreateBehaviour()
        {
            var gameServices = ServiceProviderAccessor.Instance.ForScene(SceneNames.Game);
            return new RemoveFromBallsOnFieldBehavior(gameServices.GetRequiredService<BallsOnField>());
        }
    }
}