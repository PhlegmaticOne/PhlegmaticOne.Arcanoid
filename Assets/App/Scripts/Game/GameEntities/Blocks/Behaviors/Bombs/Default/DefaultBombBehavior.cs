using System.Collections.Generic;
using Game.Field;
using Game.GameEntities.Blocks.Behaviors.Bombs.Common;

namespace Game.GameEntities.Blocks.Behaviors.Bombs.Default
{
    public class DefaultBombBehavior : BombBehaviorBase
    {
        public DefaultBombBehavior(GameField gameField, BombConfiguration bombBlockConfiguration) : 
            base(gameField, bombBlockConfiguration) { }

        protected override List<FieldPosition> GetAffectingPositions(in FieldPosition bombPosition)
        {
            var result = new List<FieldPosition>
            {
                bombPosition.Up(),
                bombPosition.Down(),
                bombPosition.Left(),
                bombPosition.Right(),
                bombPosition.LeftUp(),
                bombPosition.RightUp(),
                bombPosition.LeftDown(),
                bombPosition.RightDown()
            };
            return result;
        }
    }
}