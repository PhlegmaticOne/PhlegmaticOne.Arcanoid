using Game.GameEntities.Blocks;
using Game.GameEntities.Bonuses;
using Game.GameEntities.PlayerObjects.BallObject;
using Libs.Pooling;
using Libs.Pooling.Implementation;
using Libs.Popups;
using Libs.Services;
using UnityEngine;

namespace Common.ServiceInstallers
{
    public class PoolProviderInstaller : ServiceInstaller
    {
        [SerializeField] private PopupComposite _popupComposite;
        [SerializeField] private UnityObjectPoolInstaller<Ball> _ballPoolInstaller;
        [SerializeField] private UnityObjectPoolInstaller<Block> _blockPoolInstaller;
        [SerializeField] private UnityObjectPoolInstaller<Bonus> _bonusPoolInstaller;
        [SerializeField] private PooledObjectsContainer _pooledObjectsContainer;
        [SerializeField] private PopupContainer _popupContainer;
        
        public override void InstallServices(IServiceCollection serviceCollection)
        {
            var poolBuilder = PoolBuilder.Create();
            var popupContainer = Instantiate(_popupContainer);
            var poolContainer = Instantiate(_pooledObjectsContainer);
            
            _popupComposite.AddPopupsToPool(poolBuilder, popupContainer.CanvasTransform);
            poolBuilder.AddPool(_ballPoolInstaller.CreateObjectPool(poolContainer.transform));
            poolBuilder.AddPool(_blockPoolInstaller.CreateObjectPool(poolContainer.transform));
            poolBuilder.AddPool(_bonusPoolInstaller.CreateObjectPool(poolContainer.transform));
            
            var poolProvider = poolBuilder.BuildProvider();

            serviceCollection.AddSingleton(poolProvider);
        }
    }
}