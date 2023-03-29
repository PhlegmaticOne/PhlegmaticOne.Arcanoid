using Libs.Behaviors;
using Libs.Behaviors.Installer;
using UnityEngine;

namespace Game.GameEntities.Blocks.Behaviors.Common.Shake
{
    public class ShakeBehaviorInstaller : BehaviorInstaller<Block>
    {
        [SerializeField] private float _time;
        [SerializeField] private Vector3 _force;
        [SerializeField] private int _vibrato;
        
        public override IObjectBehavior<Block> CreateBehaviour()
        {
            var behavior = new ShakeBlockBehavior();
            behavior.SetBehaviorParameters(_time, _force, _vibrato);
            return behavior;
        }
    }
}