using Common.Scenes;
using Game.Field;
using Game.GameEntities.Blocks;
using Game.GameEntities.Blocks.Behaviors.Bomb;
using Libs.Behaviors;
using Libs.Behaviors.Installer;
using Libs.Services;
using UnityEngine;

namespace Game.GameEntities.Behaviors.Bomb
{
    public class BombBlockDestroyBehaviorInstaller : BehaviorInstaller<Block>
    {
        [SerializeField] private BombBlockConfiguration _bombBlockConfiguration;
        public override IObjectBehavior<Block> CreateBehaviour()
        {
            var gameServices = ServiceProviderAccessor.Instance.ForScene(SceneIndexes.GameScene);
            return new BombBlocksDestroyBehavior(gameServices.GetRequiredService<GameField>(), _bombBlockConfiguration);
        }
    }
}