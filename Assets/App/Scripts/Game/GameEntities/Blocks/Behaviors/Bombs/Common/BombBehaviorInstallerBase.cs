using Common.Scenes;
using Game.Field;
using Libs.Behaviors;
using Libs.Behaviors.Installer;
using Libs.Services;
using UnityEngine;

namespace Game.GameEntities.Blocks.Behaviors.Bombs.Common
{
    public abstract class BombBehaviorInstallerBase : BehaviorInstaller<Block>
    {
        [SerializeField] private BombConfiguration _bombConfiguration;
        public override IObjectBehavior<Block> CreateBehaviour()
        {
            var gameServices = ServiceProviderAccessor.Instance.ForScene(SceneIndexes.GameScene);
            return CreateBombBehavior(gameServices.GetRequiredService<GameField>(), _bombConfiguration);
        }

        protected abstract BombBehaviorBase CreateBombBehavior(GameField gameField,
            BombConfiguration bombConfiguration);
    }
}