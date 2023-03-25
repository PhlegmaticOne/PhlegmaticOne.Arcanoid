using Game.Logic.Systems.Control;
using Libs.Behaviors;
using UnityEngine;

namespace Game.GameEntities.PlayerObjects.BallObject.Behaviors.Bottom
{
    public class ReturnToPlatformCommand : IObjectBehavior<Ball>
    {
        private readonly ControlSystem _controlSystem;
        private readonly BallsOnField _ballsOnField;

        public ReturnToPlatformCommand(ControlSystem controlSystem, BallsOnField ballsOnField)
        {
            _controlSystem = controlSystem;
            _ballsOnField = ballsOnField;
        }
        public bool IsDefault => true;

        public void Behave(Ball entity, Collision2D collision2D)
        {
            entity.SetStartSpeed(entity.GetSpeed().magnitude);
            entity.SetSpeed(Vector2.zero);
            _ballsOnField.Add(entity);
            _controlSystem.AddObjectToFollow(entity);
        }
    }
}