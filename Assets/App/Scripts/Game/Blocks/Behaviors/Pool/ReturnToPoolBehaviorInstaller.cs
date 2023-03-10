using Game.Accessors;
using Game.Behaviors;
using Game.Behaviors.Installer;
using Game.Field;
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
            var fieldAccessor = serviceProvider.GetRequiredService<IObjectAccessor<GameField>>();
            return new ReturnToPoolBehavior(poolProvider, fieldAccessor);
        }
    }
}