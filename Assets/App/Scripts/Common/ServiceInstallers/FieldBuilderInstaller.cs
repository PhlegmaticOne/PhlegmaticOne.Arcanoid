using Game.Blocks.Spawners;
using Game.Field.Builder;
using Game.Field.Helpers;
using Libs.Services;
using UnityEngine;

namespace Common.ServiceInstallers
{
    public class FieldBuilderInstaller : ServiceInstaller
    {
        [SerializeField] private FieldPositionsGenerator _fieldPositionsGenerator;
        public override void InstallServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IFieldBuilder>(x => new FieldBuilder(x.GetRequiredService<IBlockSpawner>(),
                _fieldPositionsGenerator));
        }
    }
}