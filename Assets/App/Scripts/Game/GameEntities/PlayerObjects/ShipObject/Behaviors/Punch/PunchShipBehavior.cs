using DG.Tweening;
using Game.GameEntities.PlayerObjects.BallObject;
using Libs.Behaviors;
using UnityEngine;

namespace Game.GameEntities.PlayerObjects.ShipObject.Behaviors.Punch
{
    public class PunchShipBehavior : IObjectBehavior<Ship>
    {
        public bool IsDefault => true;

        private Vector2 _punchDirection;
        private float _time;

        public void SetBehaviorParameters(Vector2 punchDirection, float time)
        {
            _time = time;
            _punchDirection = punchDirection;
        }
        
        public void Behave(Ship entity, Collision2D collision2D)
        {
            if (collision2D.collider.TryGetComponent<Ball>(out var ball))
            {
                if (!(ball.GetSpeed().y > 1))
                {
                    return;
                }
                
                TryPunch(entity);
            }
        }

        private void TryPunch(Ship entity)
        {
            if (DOTween.IsTweening(entity.ActivePartTransform))
            {
                return;
            }
            
            entity.ActivePartTransform
                .DOPunchPosition(_punchDirection, _time, 0, 0)
                .SetUpdate(true)
                .Play();
            
            entity.UnactivePartTransform
                .DOPunchPosition(_punchDirection, _time, 0, 0)
                .SetUpdate(true)
                .Play();
        }
    }
}