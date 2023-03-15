using Libs.Behaviors;
using Libs.Behaviors.Installer;
using Libs.Pooling.Base;
using Libs.Services;

namespace Game.GameEntities.PlayerObjects.BallObject.Behaviors.Bottom
{
    public class ReturnBallToPoolBehaviorInstaller : BehaviorInstaller<Ball>
    {
        public override IObjectBehavior<Ball> CreateBehaviour()
        {
            var global = ServiceProviderAccessor.Global;
            return new ReturnBallToPoolBehavior(global.GetRequiredService<IPoolProvider>());
        }
    }
}