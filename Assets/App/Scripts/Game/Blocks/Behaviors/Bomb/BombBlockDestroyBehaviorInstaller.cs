using Common.Scenes;
using Game.Behaviors;
using Game.Behaviors.Installer;
using Game.Field;
using Libs.Services;
using UnityEngine;

namespace Game.Blocks.Behaviors.Bomb
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