using Common.Scenes;
using Game.Behaviors;
using Game.Behaviors.Installer;
using Game.Field;
using Libs.Pooling.Base;
using Libs.Services;

namespace Game.Blocks.Behaviors.Common.ReturnToPool
{
    public class ReturnToPoolBehaviorInstaller : BehaviorInstaller<Block>
    {
        public override IObjectBehavior<Block> CreateBehaviour()
        {
            var global = ServiceProviderAccessor.Global;
            var game = ServiceProviderAccessor.Instance.ForScene(SceneIndexes.GameScene);
            
            var poolProvider = global.GetRequiredService<IPoolProvider>();
            var fieldAccessor = game.GetRequiredService<GameField>();
            return new ReturnToPoolBehavior(poolProvider, fieldAccessor);
        }
    }
}