using Game.Field;
using Game.GameEntities.Blocks.Behaviors.Bombs.Common;
using UnityEngine;

namespace Game.GameEntities.Blocks.Behaviors.Bombs.Axis
{
    public class AxisBombBehaviorInstaller : BombBehaviorInstallerBase
    {
        [SerializeField] private AxisType _axisType;
        protected override BombBehaviorBase CreateBombBehavior(GameField gameField, BombConfiguration bombConfiguration)
        {
            var behavior = new AxisBombBehavior(gameField, bombConfiguration);
            behavior.SetBehaviorParameters(_axisType);
            return behavior;
        }
    }
}