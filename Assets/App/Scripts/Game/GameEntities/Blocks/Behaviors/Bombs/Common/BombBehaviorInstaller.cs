using Common.Scenes;
using Game.Field;
using Game.GameEntities.Blocks.Behaviors.Bombs.Common.PositionsStrategies.Installers;
using Game.GameEntities.Blocks.Behaviors.Bombs.Common.TimeActions.Installer;
using Libs.Behaviors;
using Libs.Behaviors.Installer;
using Libs.Services;
using Libs.TimeActions;
using UnityEngine;

namespace Game.GameEntities.Blocks.Behaviors.Bombs.Common
{
    public class BombBehaviorInstaller : BehaviorInstaller<Block>
    {
        [SerializeField] private BombConfiguration _bombConfiguration;
        [SerializeField] private BombTimeActionInstallerBase _bombTimeActionInstaller;
        [SerializeField] private BombPositionsSearcherInstallerBase _bombPositionsSearcherInstaller;
        
        public override IObjectBehavior<Block> CreateBehaviour()
        {
            var gameServices = ServiceProviderAccessor.Instance.ForScene(SceneNames.Game);
            var timeActionsManager = gameServices.GetRequiredService<TimeActionsManager>();
            var gameField = gameServices.GetRequiredService<GameField>();
            var bombTimeAction = _bombTimeActionInstaller.CreateBombTimeAction(_bombConfiguration, gameField);
            var positionsSearcher = _bombPositionsSearcherInstaller
                .CreateBombPositionsSearcher(_bombConfiguration, gameField);
            return new BombBehavior(gameField, positionsSearcher, timeActionsManager, bombTimeAction);
        }
    }
}