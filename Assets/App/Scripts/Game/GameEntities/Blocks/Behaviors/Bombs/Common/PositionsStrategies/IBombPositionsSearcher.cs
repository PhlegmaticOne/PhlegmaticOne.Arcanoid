using System.Collections.Generic;
using Game.Field;

namespace Game.GameEntities.Blocks.Behaviors.Bombs.Common.PositionsStrategies
{
    public interface IBombPositionsSearcher
    {
        List<FieldPosition> FindBombAffectingPositions(FieldPosition startPosition);
    }
}