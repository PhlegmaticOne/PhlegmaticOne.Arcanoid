using Common.Scenes;
using Game.Field;
using Libs.Behaviors;
using Libs.Behaviors.Installer;
using Libs.Services;
using UnityEngine;

namespace Game.GameEntities.Blocks.Behaviors.ChainBomb
{
    public class ChainBombDestroyBehaviorInstaller : BehaviorInstaller<Block>
    {
        [SerializeField] private ChainBombConfiguration _chainBombConfiguration;
        public override IObjectBehavior<Block> CreateBehaviour()
        {
            var gameServices = ServiceProviderAccessor.Instance.ForScene(SceneIndexes.GameScene);
            return new ChainBombDestroyBehavior(gameServices.GetRequiredService<GameField>(), _chainBombConfiguration);
        }
    }
}