using Common.Scenes;
using Game.Behaviors;
using Game.Behaviors.Installer;
using Game.Systems.Control;
using Libs.Services;

namespace Game.PlayerObjects.BallObject.Behaviors.Bottom
{
    public class ReturnToPlatformBehaviorInstaller : BehaviorInstaller<Ball>
    {
        public override IObjectBehavior<Ball> CreateBehaviour()
        {
            var game = ServiceProviderAccessor.Instance.ForScene(SceneIndexes.GameScene);
            
            var controlSystemAccessor = game.GetRequiredService<ControlSystem>();
            return new ReturnToPlatformCommand(controlSystemAccessor);
        }
    }
}