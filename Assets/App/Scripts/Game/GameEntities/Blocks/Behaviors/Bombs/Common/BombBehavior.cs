using System.Collections.Generic;
using Game.Field;
using Game.GameEntities.Blocks.Behaviors.Bombs.Common.PositionsStrategies;
using Game.GameEntities.Blocks.Behaviors.Bombs.Common.TimeActions.Base;
using Libs.Behaviors;
using Libs.TimeActions;
using UnityEngine;

namespace Game.GameEntities.Blocks.Behaviors.Bombs.Common
{
    public class BombBehavior : IObjectBehavior<Block>
    {
        private readonly GameField _gameField;
        private readonly IBombPositionsSearcher _bombPositionsSearcher;
        private readonly TimeActionsManager _timeActionsManager;
        private readonly IBombTimeAction _bombTimeAction;

        public BombBehavior(GameField gameField,
            IBombPositionsSearcher bombPositionsSearcher,
            TimeActionsManager timeActionsManager,
            IBombTimeAction bombTimeAction)
        {
            _gameField = gameField;
            _bombPositionsSearcher = bombPositionsSearcher;
            _timeActionsManager = timeActionsManager;
            _bombTimeAction = bombTimeAction;
        }
        
        public bool IsDefault => false;
        
        public void Behave(Block entity, Collision2D collision2D)
        {
            var bombPosition = _gameField.GetBlockPosition(entity);

            if (bombPosition == FieldPosition.None)
            {
                return;
            }

            var positions = _bombPositionsSearcher.FindBombAffectingPositions(bombPosition);
            ApplyBombToPositions(positions, collision2D);
        }

        private void ApplyBombToPositions(List<FieldPosition> positions, Collision2D original)
        {
            _bombTimeAction.SetOriginalCollision(original);
            _bombTimeAction.SetAffectingPositions(positions);
            _timeActionsManager.AddTimeAction(_bombTimeAction);
        }
    }
}