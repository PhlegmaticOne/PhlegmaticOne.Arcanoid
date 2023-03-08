using Game.Blocks.Configurations;
using Game.Blocks.Spawners;
using Libs.Pooling.Base;
using Libs.Services;
using UnityEngine;

namespace Common.ServiceInstallers
{
    public class BlockSpawnerInstaller : ServiceInstaller
    {
        [SerializeField] private BlockSpawnSystemConfiguration _blockSpawnSystemConfiguration;
        public override void InstallServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IBlockSpawner>(s =>
                new BlockSpawner(s.GetRequiredService<IPoolProvider>(), _blockSpawnSystemConfiguration));
        }
    }
}