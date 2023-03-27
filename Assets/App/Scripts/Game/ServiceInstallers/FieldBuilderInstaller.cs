using Game.Field;
using Game.Field.Builder;
using Game.Field.Helpers;
using Game.GameEntities.Blocks.Spawners;
using Libs.Services;
using UnityEngine;

namespace Game.ServiceInstallers
{
    public class FieldBuilderInstaller : ServiceInstaller
    {
        [SerializeField] private FieldPositionsGenerator _fieldPositionsGenerator;
        [SerializeField] private Transform _pointTransform;
        [SerializeField] private DynamicBuildingInfo _dynamicBuildingInfo;
        public override void InstallServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IFieldBuilder>(x =>
            {
                var blockSpawner = x.GetRequiredService<IBlockSpawner>();
                var gameField = x.GetRequiredService<GameField>();
                return new FieldBuilder(blockSpawner, _fieldPositionsGenerator,
                    gameField, _pointTransform, _dynamicBuildingInfo);
            });
        }
    }
}