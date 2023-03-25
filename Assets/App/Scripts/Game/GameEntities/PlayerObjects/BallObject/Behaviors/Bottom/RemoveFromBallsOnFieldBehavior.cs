using Libs.Behaviors;
using UnityEngine;

namespace Game.GameEntities.PlayerObjects.BallObject.Behaviors.Bottom
{
    public class RemoveFromBallsOnFieldBehavior : IObjectBehavior<Ball>
    {
        private readonly BallsOnField _ballsOnField;

        public RemoveFromBallsOnFieldBehavior(BallsOnField ballsOnField) => _ballsOnField = ballsOnField;
        public bool IsDefault => true;
        public void Behave(Ball entity, Collision2D collision2D) => _ballsOnField.Remove(entity);
    }
}