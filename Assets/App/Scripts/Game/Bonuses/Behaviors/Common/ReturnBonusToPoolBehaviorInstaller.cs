
using Common.Scenes;
using Game.Behaviors;
using Game.Behaviors.Installer;
using Libs.Pooling.Base;
using Libs.Services;

namespace Game.Bonuses.Behaviors.Common
{
    public class ReturnBonusToPoolBehaviorInstaller : BehaviorInstaller<Bonus>
    {
        public override IObjectBehavior<Bonus> CreateBehaviour()
        {
            var gameServices = ServiceProviderAccessor.Instance.ForScene(SceneIndexes.GameScene);
            var global = ServiceProviderAccessor.Global;

            var poolProvider = global.GetRequiredService<IPoolProvider>();
            var bonusesOnField = gameServices.GetRequiredService<BonusesOnField>();

            return new ReturnBonusToPoolBehavior(poolProvider, bonusesOnField);
        }
    }
}