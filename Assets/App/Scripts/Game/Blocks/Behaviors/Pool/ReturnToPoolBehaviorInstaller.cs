using Game.Behaviors;
using Game.Behaviors.Installer;
using Libs.Pooling.Base;
using Libs.Services;

namespace Game.Blocks.Behaviors.Pool
{
    public class ReturnToPoolBehaviorInstaller : BehaviorInstaller<Block>
    {
        public override IObjectBehavior<Block> CreateBehaviour()
        {
            var serviceProvider = ServiceProviderAccessor.ServiceProvider;
            var poolProvider = serviceProvider.GetRequiredService<IPoolProvider>();
            return new ReturnToPoolBehavior(poolProvider);
        }
    }
}