using System.Collections.Generic;
using Game.Field;
using Libs.TimeActions.Base;
using UnityEngine;

namespace Game.GameEntities.Blocks.Behaviors.Bombs.Common.TimeActions.Base
{
    public interface IBombTimeAction : ITimeAction
    {
        void SetAffectingPositions(List<FieldPosition> affectingPositions);
        void SetOriginalCollision(Collision2D original);
    }
}