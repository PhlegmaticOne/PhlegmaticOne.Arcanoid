using Libs.Behaviors;
using Libs.Behaviors.Installer;
using UnityEngine;

namespace Game.GameEntities.PlayerObjects.BallObject.Behaviors.Bottom
{
    public class CameraShakeBehaviorInstaller : BehaviorInstaller<Ball>
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Vector2 _direction;
        [SerializeField] private float _time;
        [SerializeField] private int _vibrato;
        public override IObjectBehavior<Ball> CreateBehaviour()
        {
            var behavior = new CameraShakeBehavior(_camera);
            behavior.SetBehaviorParameters(_direction, _time, _vibrato);
            return behavior;
        }
    }
}