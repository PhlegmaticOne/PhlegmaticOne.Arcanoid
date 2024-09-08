using System.Collections.Generic;
using Game.Field;
using Game.GameEntities.Blocks.Behaviors.Bombs.Common.PositionsStrategies;

namespace Game.GameEntities.Blocks.Behaviors.Bombs.Axis
{
    public class AxisBombPositionsSearcher : IBombPositionsSearcher
    {
        private readonly AxisType _axisType;
        private readonly GameField _gameField;

        public AxisBombPositionsSearcher(AxisType axisType, GameField gameField)
        {
            _axisType = axisType;
            _gameField = gameField;
        }
        
        public List<FieldPosition> FindBombAffectingPositions(FieldPosition startPosition)
        {
            var movePositions = GetMovePositions();
            var result = new List<FieldPosition>();

            foreach (var movePosition in movePositions)
            {
                AddPositionsWhileInField(result, startPosition, movePosition);
            }

            return result;
        }
        

        private void AddPositionsWhileInField(List<FieldPosition> positions, 
            in FieldPosition startPosition, in FieldPosition direction)
        {
            var start = startPosition + direction;
            
            while (_gameField.ContainsPosition(start))
            {
                positions.Add(start);
                start += direction;
            }
        }

        private List<FieldPosition> GetMovePositions()
        {
            var movePositions = new List<FieldPosition>();
            
            switch (_axisType)
            {
                case AxisType.Horizontal:
                    movePositions.Add(FieldPosition.LeftDirection);
                    movePositions.Add(FieldPosition.RightDirection);
                    break;
                case AxisType.Vertical:
                    movePositions.Add(FieldPosition.DownDirection);
                    movePositions.Add(FieldPosition.UpDirection);
                    break;
            }

            return movePositions;
        }
    }
}