using Game.Logic.Systems.Control;
using Libs.Behaviors;
using UnityEngine;

namespace Game.GameEntities.PlayerObjects.BallObject.Behaviors.Bottom
{
    public class ReturnToPlatformCommand : IObjectBehavior<Ball>
    {
        private readonly ControlSystem _controlSystem;

        public ReturnToPlatformCommand(ControlSystem controlSystem) => _controlSystem = controlSystem;

        public void Behave(Ball entity, Collision2D collision2D)
        {
            entity.SetStartSpeed(entity.GetSpeed().magnitude);
            entity.SetDirection(Vector2.up);
            entity.SetSpeed(Vector2.zero);
            _controlSystem.AddObjectToFollow(entity);
        }
    }
}