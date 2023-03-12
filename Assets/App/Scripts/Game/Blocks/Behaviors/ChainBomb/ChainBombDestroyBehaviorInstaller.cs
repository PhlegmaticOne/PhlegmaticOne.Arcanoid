using Common.Scenes;
using Game.Behaviors;
using Game.Behaviors.Installer;
using Game.Field;
using Libs.Services;
using UnityEngine;

namespace Game.Blocks.Behaviors.ChainBomb
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