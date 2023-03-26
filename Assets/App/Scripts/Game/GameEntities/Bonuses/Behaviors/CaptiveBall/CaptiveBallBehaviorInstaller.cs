using Common.Scenes;
using Game.GameEntities.Blocks;
using Game.GameEntities.PlayerObjects.BallObject;
using Game.GameEntities.PlayerObjects.BallObject.Spawners;
using Libs.Behaviors;
using Libs.Behaviors.Installer;
using Libs.Services;

namespace Game.GameEntities.Bonuses.Behaviors.CaptiveBall
{
    public class CaptiveBallBehaviorInstaller : BehaviorInstaller<Block>
    {
        public override IObjectBehavior<Block> CreateBehaviour()
        {
            var gameServices = ServiceProviderAccessor.Instance.ForScene(SceneNames.Game);

            var balls = gameServices.GetRequiredService<BallsOnField>();
            var ballSpawner = gameServices.GetRequiredService<IBallSpawner>();
            var captiveBallsSystem = gameServices.GetRequiredService<CaptiveBallsSystem>();

            return new CaptiveBallBehavior(ballSpawner, balls, captiveBallsSystem);
        }
    }
}