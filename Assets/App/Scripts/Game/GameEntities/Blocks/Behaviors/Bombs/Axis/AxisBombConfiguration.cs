using Game.GameEntities.Blocks.Behaviors.Bombs.Common;
using Game.GameEntities.Blocks.Configurations;
using UnityEngine;

namespace Game.GameEntities.Blocks.Behaviors.Bombs.Axis
{
    [CreateAssetMenu(menuName = "Game/Bombs/Behaviors/Create axis bomb configuration", order = 0)]
    public class AxisBombConfiguration : BombConfiguration
    {
        public override BlockAffectingType GetAffectingType(BlockConfiguration blockConfiguration)
        {
            return _blockAffecting;
        }
    }
}