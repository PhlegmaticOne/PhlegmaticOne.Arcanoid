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
            var game = ServiceProviderAccessor.Instance.ForScene(SceneIndexes.GameScene);
            
            var controlSystemAccessor = game.GetRequiredService<ControlSystem>();
            return new ReturnToPlatformCommand(controlSystemAccessor);
        }
    }
}