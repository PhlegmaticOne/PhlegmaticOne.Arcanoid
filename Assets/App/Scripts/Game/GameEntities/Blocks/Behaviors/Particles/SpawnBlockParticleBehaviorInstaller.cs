using Common.Scenes;
using Game.ObjectParticles;
using Libs.Behaviors;
using Libs.Behaviors.Installer;
using Libs.Services;
using UnityEngine;

namespace Game.GameEntities.Blocks.Behaviors.Particles
{
    public class SpawnBlockParticleBehaviorInstaller : BehaviorInstaller<Block>
    {
        [SerializeField] private Color _particleColor;
        
        public override IObjectBehavior<Block> CreateBehaviour()
        {
            var gameServices = ServiceProviderAccessor.Instance.ForScene(SceneIndexes.GameScene);
            var behavior = new SpawnBlockParticleBehavior(gameServices.GetRequiredService<ParticleManager>());
            behavior.SetBehaviorParameters(_particleColor);
            return behavior;
        }
    }
}