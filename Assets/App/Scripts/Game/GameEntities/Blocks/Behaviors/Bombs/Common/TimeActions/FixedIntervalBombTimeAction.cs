using System.Collections.Generic;
using Game.Field;
using Game.GameEntities.Blocks.Behaviors.Bombs.Common.AffectStrategies;
using Game.GameEntities.Blocks.Behaviors.Bombs.Common.TimeActions.Base;
using Libs.TimeActions.Base;
using UnityEngine;

namespace Game.GameEntities.Blocks.Behaviors.Bombs.Common.TimeActions
{
    public class FixedIntervalBombTimeAction : FixedIntervalTimeAction, IBombTimeAction
    {
        private readonly IBlockAffectStrategy _blockAffectStrategy;
        private List<FieldPosition> _affectingPositions;
        private Collision2D _original;

        public FixedIntervalBombTimeAction(float fixedInterval,
            IBlockAffectStrategy blockAffectStrategy) : base(fixedInterval, 0)
        {
            _blockAffectStrategy = blockAffectStrategy;
        }

        public void SetAffectingPositions(List<FieldPosition> affectingPositions) => 
            _affectingPositions = affectingPositions;

        public void SetOriginalCollision(Collision2D original) => 
            _original = original;

        public override void OnStart() => SetActionsCount(_affectingPositions.Count);

        public override void OnEnd()
        {
            _affectingPositions = null;
            _original = null;
        }
        

        protected override void OnInterval(int interval)
        {
            if (_affectingPositions == null || interval >= _affectingPositions.Count)
            {
                return;
            }

            var position = _affectingPositions[interval];
            _blockAffectStrategy.AffectBlockAtPosition(position, _original);
        }
    }
}