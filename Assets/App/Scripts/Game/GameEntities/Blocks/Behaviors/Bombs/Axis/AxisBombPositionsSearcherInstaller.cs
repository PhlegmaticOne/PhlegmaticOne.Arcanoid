using Game.Field;
using Game.GameEntities.Blocks.Behaviors.Bombs.Common;
using Game.GameEntities.Blocks.Behaviors.Bombs.Common.PositionsStrategies;
using Game.GameEntities.Blocks.Behaviors.Bombs.Common.PositionsStrategies.Installers;
using UnityEngine;

namespace Game.GameEntities.Blocks.Behaviors.Bombs.Axis
{
    public class AxisBombPositionsSearcherInstaller : BombPositionsSearcherInstallerBase
    {
        [SerializeField] private AxisType _axisType;
        public override IBombPositionsSearcher CreateBombPositionsSearcher(BombConfiguration bombConfiguration, 
            GameField gameField) => new AxisBombPositionsSearcher(_axisType, gameField);
    }
}