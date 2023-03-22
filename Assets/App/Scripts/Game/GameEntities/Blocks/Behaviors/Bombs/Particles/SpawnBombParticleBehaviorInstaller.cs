using Common.Scenes;
using Game.ObjectParticles;
using Libs.Behaviors;
using Libs.Behaviors.Installer;
using Libs.Services;

namespace Game.GameEntities.Blocks.Behaviors.Bombs.Particles
{
    public class SpawnBombParticleBehaviorInstaller : BehaviorInstaller<Block>
    {
        public override IObjectBehavior<Block> CreateBehaviour()
        {
            var gameServices = ServiceProviderAccessor.Instance.ForScene(SceneIndexes.GameScene);
            var particleManager = gameServices.GetRequiredService<ParticleManager>();
            return new SpawnBombParticleBehavior(particleManager);
        }
    }
}