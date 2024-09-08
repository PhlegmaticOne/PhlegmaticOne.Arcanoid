using Game.Field;
using UnityEngine;

namespace Game.GameEntities.Blocks.Behaviors.Bombs.Common.AffectStrategies
{
    public interface IBlockAffectStrategy
    {
        void AffectBlockAtPosition(FieldPosition fieldPosition, Collision2D original);
    }
}