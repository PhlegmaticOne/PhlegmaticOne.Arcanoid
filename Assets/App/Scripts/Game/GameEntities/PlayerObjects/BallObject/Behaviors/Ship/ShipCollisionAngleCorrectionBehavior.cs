using Libs.Behaviors;
using UnityEngine;

namespace Game.GameEntities.PlayerObjects.BallObject.Behaviors.Ship
{
    public class ShipCollisionAngleCorrectionBehavior : IObjectBehavior<Ball>
    {
        public void Behave(Ball entity, Collision2D collision2D)
        {
            var x = HitFactor(entity.transform.position, collision2D.transform.position, collision2D.collider.bounds.size.x);
            var direction = new Vector2(x, 1).normalized;
            entity.SetSpeed(direction * entity.GetSpeed().magnitude);
        }
        
        private static float HitFactor(Vector2 ballPosition, Vector2 racketPos, float racketWidth) => 
            (ballPosition.x - racketPos.x) / racketWidth;
    }
}