using Game.Behaviors;
using Game.Systems.Control;
using UnityEngine;

namespace Game.PlayerObjects.BallObject.Behaviors.Bottom
{
    public class ReturnToPlatformCommand : IObjectBehavior<Ball>
    {
        private readonly ControlSystem _controlSystem;

        public ReturnToPlatformCommand(ControlSystem controlSystem) => _controlSystem = controlSystem;

        public void Behave(Ball entity, Collision2D collision2D)
        {
            entity.SetSpeed(Vector2.zero);
            _controlSystem.AddObjectToFollow(entity);
        }
    }
}