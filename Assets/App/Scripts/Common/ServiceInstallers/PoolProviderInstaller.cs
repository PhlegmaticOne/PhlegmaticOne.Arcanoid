using Game.GameEntities.Blocks;
using Game.GameEntities.Bonuses;
using Game.GameEntities.Bullets;
using Game.GameEntities.PlayerObjects.BallObject;
using Game.ObjectParticles.Particles;
using Libs.Pooling;
using Libs.Pooling.Implementation;
using Libs.Popups;
using Libs.Services;
using UnityEngine;

namespace Common.ServiceInstallers
{
    public class PoolProviderInstaller : ServiceInstaller
    {
        [SerializeField] private UnityObjectPoolInstaller<Ball> _ballPoolInstaller;
        [SerializeField] private UnityObjectPoolInstaller<Block> _blockPoolInstaller;
        [SerializeField] private UnityObjectPoolInstaller<Bonus> _bonusPoolInstaller;
        [SerializeField] private UnityObjectPoolInstaller<BlockParticle> _blockParticlesPoolInstaller;
        [SerializeField] private UnityObjectPoolInstaller<BombParticle> _bombParticlesPoolInstaller;
        [SerializeField] private UnityObjectPoolInstaller<Bullet> _bulletPoolInstaller;
        [SerializeField] private PopupComposite _popupComposite;
        [SerializeField] private PopupContainer _popupContainer;
        [SerializeField] private PooledObjectsContainer _pooledObjectsContainer;
        
        public override void InstallServices(IServiceCollection serviceCollection)
        {
            var poolBuilder = PoolBuilder.Create();
            var popupContainer = Instantiate(_popupContainer);
            var poolContainer = Instantiate(_pooledObjectsContainer);
            
            _popupComposite.AddPopupsToPool(poolBuilder, popupContainer.CanvasTransform);
            poolBuilder.AddPool(_ballPoolInstaller.CreateObjectPool(poolContainer.transform));
            poolBuilder.AddPool(_blockPoolInstaller.CreateObjectPool(poolContainer.transform));
            poolBuilder.AddPool(_bonusPoolInstaller.CreateObjectPool(poolContainer.transform));
            poolBuilder.AddPool(_blockParticlesPoolInstaller.CreateObjectPool(poolContainer.transform));
            poolBuilder.AddPool(_bombParticlesPoolInstaller.CreateObjectPool(poolContainer.transform));
            poolBuilder.AddPool(_bulletPoolInstaller.CreateObjectPool(poolContainer.transform));
            
            var poolProvider = poolBuilder.BuildProvider();

            serviceCollection.AddSingleton(poolProvider);
        }
    }
}