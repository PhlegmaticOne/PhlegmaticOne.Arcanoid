using System.Collections.Generic;
using Game.Field;
using Game.GameEntities.Blocks.Behaviors.Bombs.Common;

namespace Game.GameEntities.Blocks.Behaviors.Bombs.Axis
{
    public class AxisBombBehavior : BombBehaviorBase
    {
        private AxisType _axisType;
        
        public AxisBombBehavior(GameField gameField, BombConfiguration bombBlockConfiguration) : 
            base(gameField, bombBlockConfiguration) { }

        public void SetBehaviorParameters(AxisType axisType) => _axisType = axisType;

        protected override List<FieldPosition> GetAffectingPositions(in FieldPosition bombPosition)
        {
            var movePositions = GetMovePositions();
            var result = new List<FieldPosition>();

            foreach (var movePosition in movePositions)
            {
                AddPositionsWhileInField(result, bombPosition, movePosition);
            }

            return result;
        }

        private void AddPositionsWhileInField(List<FieldPosition> positions, 
            in FieldPosition startPosition, in FieldPosition direction)
        {
            var start = startPosition + direction;
            
            while (GameField.ContainsPosition(start))
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