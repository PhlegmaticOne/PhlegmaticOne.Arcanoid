using Game.Field;
using Game.GameEntities.Blocks.Behaviors.Bombs.Common.AffectStrategies;
using Game.GameEntities.Blocks.Behaviors.Bombs.Common.TimeActions.Base;
using UnityEngine;

namespace Game.GameEntities.Blocks.Behaviors.Bombs.Common.TimeActions.Installer
{
    public class FixedIntervalBombTimeActionInstaller : BombTimeActionInstallerBase
    {
        [SerializeField] [Range(0f, 1f)] private float _fixedInterval;
        
        public override IBombTimeAction CreateBombTimeAction(BombConfiguration bombConfiguration, GameField gameField) => 
            new FixedIntervalBombTimeAction(_fixedInterval, new BlockAffectStrategy(bombConfiguration, gameField));
    }
}