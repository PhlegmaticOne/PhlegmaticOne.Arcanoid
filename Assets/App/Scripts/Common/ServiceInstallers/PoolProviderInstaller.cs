using Game.Blocks;
using Game.PlayerObjects.BallObject;
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
        
        public override void InstallServices(IServiceCollection serviceCollection)
        {
            var poolBuilder = PoolBuilder.Create();
            
            _popupComposite.AddPopupsToPool(poolBuilder);
            poolBuilder.AddPool(_ballPoolInstaller.CreateObjectPool());
            poolBuilder.AddPool(_blockPoolInstaller.CreateObjectPool());
            
            var poolProvider = poolBuilder.BuildProvider();

            serviceCollection.AddSingleton(poolProvider);
        }
    }
}