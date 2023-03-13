using Common.Scenes;
using Game.Field;
using Libs.Behaviors;
using Libs.Behaviors.Installer;
using Libs.Pooling.Base;
using Libs.Services;

namespace Game.GameEntities.Blocks.Behaviors.Common.ReturnToPool
{
    public class ReturnBlockToPoolBehaviorInstaller : BehaviorInstaller<Block>
    {
        public override IObjectBehavior<Block> CreateBehaviour()
        {
            var global = ServiceProviderAccessor.Global;
            var game = ServiceProviderAccessor.Instance.ForScene(SceneIndexes.GameScene);
            
            var poolProvider = global.GetRequiredService<IPoolProvider>();
            var fieldAccessor = game.GetRequiredService<GameField>();
            return new ReturnBlockToPoolBehavior(poolProvider, fieldAccessor);
        }
    }
}