using Libs.Behaviors;
using Libs.Behaviors.Installer;
using Libs.Pooling.Base;
using Libs.Services;

namespace Game.GameEntities.Bullets.Behaviors.ReturnToPool
{
    public class ReturnBulletToPoolBehaviorInstaller : BehaviorInstaller<Bullet>
    {
        public override IObjectBehavior<Bullet> CreateBehaviour()
        {
            var poolProvider = ServiceProviderAccessor.Global.GetRequiredService<IPoolProvider>();
            return new ReturnBulletToPoolBehavior(poolProvider);
        }
    }
}