using Game.Field;
using Game.GameEntities.Blocks.Behaviors.Bombs.Common;
using Game.GameEntities.Blocks.Behaviors.Bombs.Common.PositionsStrategies;
using Game.GameEntities.Blocks.Behaviors.Bombs.Common.PositionsStrategies.Installers;

namespace Game.GameEntities.Blocks.Behaviors.Bombs.Default
{
    public class DefaultBombPositionsSearcherInstaller : BombPositionsSearcherInstallerBase
    {
        public override IBombPositionsSearcher CreateBombPositionsSearcher(BombConfiguration bombConfiguration,
            GameField gameField) => new DefaultBombPositionsSearcher(gameField);
    }
}