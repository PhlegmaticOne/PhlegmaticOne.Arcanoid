using Common.Scenes;
using Game.ObjectParticles;
using Libs.Behaviors;
using Libs.Behaviors.Installer;
using Libs.Services;
using UnityEngine;

namespace Game.GameEntities.PlayerObjects.BallObject.Behaviors.Particles
{
    public class SpawnSideWallParticleBehaviorInstaller : BehaviorInstaller<Ball>
    {
        [SerializeField] private ColliderTag _bottomColliderTag;
        [SerializeField] private Color _rageColor;
        [SerializeField] private Color _bottomColor;
        [SerializeField] private float _bottomSize;
        public override IObjectBehavior<Ball> CreateBehaviour()
        {
            var particleManager = ServiceProviderAccessor.Instance
                .ForScene(SceneNames.Game)
                .GetRequiredService<ParticleManager>();
            var behavior = new SpawnSideWallParticleBehavior(particleManager, _bottomColliderTag);
            behavior.SetBehaviorParameters(_rageColor, _bottomColor, _bottomSize);
            return behavior;
        }
    }
}