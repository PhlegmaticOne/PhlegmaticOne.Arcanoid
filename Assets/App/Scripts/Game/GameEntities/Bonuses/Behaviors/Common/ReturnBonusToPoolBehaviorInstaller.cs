
using Common.Scenes;
using Libs.Behaviors;
using Libs.Behaviors.Installer;
using Libs.Pooling.Base;
using Libs.Services;

namespace Game.GameEntities.Bonuses.Behaviors.Common
{
    public class ReturnBonusToPoolBehaviorInstaller : BehaviorInstaller<Bonus>
    {
        public override IObjectBehavior<Bonus> CreateBehaviour()
        {
            var gameServices = ServiceProviderAccessor.Instance.ForScene(SceneNames.Game);
            var global = ServiceProviderAccessor.Global;

            var poolProvider = global.GetRequiredService<IPoolProvider>();
            var bonusesOnField = gameServices.GetRequiredService<BonusesOnField>();

            return new ReturnBonusToPoolBehavior(poolProvider, bonusesOnField);
        }
    }
}