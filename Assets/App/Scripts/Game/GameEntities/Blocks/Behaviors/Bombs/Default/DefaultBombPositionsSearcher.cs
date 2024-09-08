using System.Collections.Generic;
using Game.Field;
using Game.GameEntities.Blocks.Behaviors.Bombs.Common.PositionsStrategies;

namespace Game.GameEntities.Blocks.Behaviors.Bombs.Default
{
    public class DefaultBombPositionsSearcher : IBombPositionsSearcher
    {
        private readonly GameField _gameField;

        public DefaultBombPositionsSearcher(GameField gameField) => _gameField = gameField;

        public List<FieldPosition> FindBombAffectingPositions(FieldPosition startPosition)
        {
            var result = new List<FieldPosition>();
            AddIfBlockExists(result, startPosition.Up());
            AddIfBlockExists(result, startPosition.LeftUp());
            AddIfBlockExists(result, startPosition.Left());
            AddIfBlockExists(result, startPosition.LeftDown());
            AddIfBlockExists(result, startPosition.Down());
            AddIfBlockExists(result, startPosition.RightDown());
            AddIfBlockExists(result, startPosition.Right());
            AddIfBlockExists(result, startPosition.RightUp());
            return result;
        }

        private void AddIfBlockExists(List<FieldPosition> addTo, in FieldPosition position)
        {
            if (_gameField.ContainsBlockAtPosition(position))
            {
                addTo.Add(position);
            }
        }
    }
}