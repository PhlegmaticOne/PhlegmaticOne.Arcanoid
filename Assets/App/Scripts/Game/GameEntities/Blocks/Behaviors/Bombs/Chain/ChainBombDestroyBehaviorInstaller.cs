using Game.Field;
using Game.GameEntities.Blocks.Behaviors.Bombs.Chain;
using Game.GameEntities.Blocks.Behaviors.Bombs.Common;

namespace Game.GameEntities.Blocks.Behaviors.ChainBomb
{
    public class ChainBombDestroyBehaviorInstaller : BombBehaviorInstallerBase
    {
        protected override BombBehaviorBase CreateBombBehavior(GameField gameField, BombConfiguration bombConfiguration) => 
            new ChainBombBehavior(gameField, bombConfiguration);
    }
}