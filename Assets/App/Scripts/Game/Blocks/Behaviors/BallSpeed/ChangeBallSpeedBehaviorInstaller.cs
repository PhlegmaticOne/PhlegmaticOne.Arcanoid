using Game.Accessors;
using Game.Behaviors;
using Game.Behaviors.Installer;
using Game.Field;
using Game.PlayerObjects.BallObject;
using Libs.Services;
using UnityEngine;

namespace Game.Blocks.Behaviors.BallSpeed
{
    public class ChangeBallSpeedBehaviorInstaller : BehaviorInstaller<Block>
    {
        [SerializeField] private float _increaseBallSpeed;
        
        public override IObjectBehavior<Block> CreateBehaviour()
        {
            var serviceProvider = ServiceProviderAccessor.ServiceProvider;
            var fieldAccessor = serviceProvider.GetRequiredService<IObjectAccessor<GameField>>();
            var ballsAccessor = serviceProvider.GetRequiredService<IObjectAccessor<BallsOnField>>();
            var behavior = new ChangeBallSpeedBehavior(fieldAccessor, ballsAccessor);
            behavior.SetBehaviorParameters(_increaseBallSpeed);
            return behavior;
        }
    }
}