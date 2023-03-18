using Game.Field;
using Game.GameEntities.Blocks.Behaviors.Bombs.Common;
using Game.GameEntities.Blocks.Behaviors.Bombs.Common.PositionsStrategies;
using Game.GameEntities.Blocks.Behaviors.Bombs.Common.PositionsStrategies.Installers;

namespace Game.GameEntities.Blocks.Behaviors.Bombs.Chain
{
    public class ChainBombPositionsSearcherInstaller : BombPositionsSearcherInstallerBase
    {
        public override IBombPositionsSearcher CreateBombPositionsSearcher(BombConfiguration bombConfiguration,
            GameField gameField) => new ChainBombPositionsSearcher(gameField, bombConfiguration);
    }
}