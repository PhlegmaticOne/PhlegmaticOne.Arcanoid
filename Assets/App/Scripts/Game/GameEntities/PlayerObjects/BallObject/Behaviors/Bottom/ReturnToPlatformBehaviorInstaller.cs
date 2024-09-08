using Common.Scenes;
using Game.Logic.Systems.Control;
using Libs.Behaviors;
using Libs.Behaviors.Installer;
using Libs.Services;

namespace Game.GameEntities.PlayerObjects.BallObject.Behaviors.Bottom
{
    public class ReturnToPlatformBehaviorInstaller : BehaviorInstaller<Ball>
    {
        public override IObjectBehavior<Ball> CreateBehaviour()
        {
            var gameService = ServiceProviderAccessor.Instance.ForScene(SceneNames.Game);
            var controlSystem = gameService.GetRequiredService<ControlSystem>();
            var ballsOnField = gameService.GetRequiredService<BallsOnField>();
            return new ReturnToPlatformCommand(controlSystem, ballsOnField);
        }
    }
}