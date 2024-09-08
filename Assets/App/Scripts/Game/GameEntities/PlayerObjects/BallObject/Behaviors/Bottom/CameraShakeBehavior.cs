using DG.Tweening;
using Libs.Behaviors;
using UnityEngine;

namespace Game.GameEntities.PlayerObjects.BallObject.Behaviors.Bottom
{
    public class CameraShakeBehavior : IObjectBehavior<Ball>
    {
        private readonly Camera _camera;
        public bool IsDefault => true;

        private Vector2 _direction;
        private float _time;
        private int _vibrato;

        public CameraShakeBehavior(Camera camera)
        {
            _camera = camera;
        }

        public void SetBehaviorParameters(Vector2 direction, float time, int vibrato)
        {
            _direction = direction;
            _time = time;
            _vibrato = vibrato;
        }

        public void Behave(Ball entity, Collision2D collision2D)
        {
            _camera.DOShakePosition(_time, _direction, _vibrato).SetUpdate(true).Play();
        }
    }
}