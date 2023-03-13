using Game.GameEntities.Blocks.Configurations;
using Game.GameEntities.Blocks.Spawners;
using Game.GameEntities.Blocks.Spawners.Configurations;
using Libs.Pooling.Base;
using Libs.Services;
using UnityEngine;

namespace Game.ServiceInstallers
{
    public class BlockSpawnerInstaller : ServiceInstaller
    {
        [SerializeField] private BlockSpawnSystemConfiguration _blockSpawnSystemConfiguration;
        [SerializeField] private BlockCracksConfiguration _blockCracksConfiguration;
        [SerializeField] private Transform _spawnTransform;
        public override void InstallServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IBlockSpawner>(s =>
            {
                var global = ServiceProviderAccessor.Global;
                return new BlockSpawner(global.GetRequiredService<IPoolProvider>(), _blockSpawnSystemConfiguration,
                    _blockCracksConfiguration, _spawnTransform);
            });
        }
    }
}