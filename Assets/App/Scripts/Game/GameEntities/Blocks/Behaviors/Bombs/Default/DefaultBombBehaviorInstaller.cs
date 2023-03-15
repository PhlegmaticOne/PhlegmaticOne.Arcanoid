using Game.Field;
using Game.GameEntities.Blocks.Behaviors.Bombs.Common;
using Game.GameEntities.Blocks.Behaviors.Bombs.Default;

namespace Game.GameEntities.Behaviors.Bomb
{
    public class DefaultBombBehaviorInstaller : BombBehaviorInstallerBase
    {
        protected override BombBehaviorBase CreateBombBehavior(GameField gameField, BombConfiguration bombConfiguration)
        {
            var bombBehavior = new DefaultBombBehavior(gameField, bombConfiguration);
            return bombBehavior;
        }
    }
}