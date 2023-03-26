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
        public override IObjectBehavior<Block> CreateBehaviour()
        {
            var gameServices = ServiceProviderAccessor.Instance.ForScene(SceneNames.Game);
            var behavior = new SpawnBlockParticleBehavior(gameServices.GetRequiredService<ParticleManager>());
            return behavior;
        }
    }
}