using Game.GameEntities.Common;
using Libs.Behaviors;
using Libs.Behaviors.Installer;

namespace Game.GameEntities.Bullets.Behaviors.Damage
{
    public class BulletDamageBlockBehaviorInstaller : BehaviorInstaller<Bullet>
    {
        public override IObjectBehavior<Bullet> CreateBehaviour()
        {
            return new DamageBlockBehavior<Bullet>();
        }
    }
}