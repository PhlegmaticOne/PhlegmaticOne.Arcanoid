﻿using Game.Common;
using Game.Field;
using Game.Field.Builder;
using Game.Field.Helpers;
using Game.GameEntities.Blocks.Spawners;
using Libs.Services;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.ServiceInstallers
{
    public class FieldBuilderInstaller : ServiceInstaller
    {
        [SerializeField] private FieldPositionsGenerator _fieldPositionsGenerator;
        [SerializeField] private Transform _pointTransform;
        [SerializeField] private FieldBuilderInfo _fieldBuilderInfo;
        public override void InstallServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IFieldBuilder>(x =>
            {
                var blockSpawner = x.GetRequiredService<IBlockSpawner>();
                var gameField = x.GetRequiredService<GameField>();
                return new FieldBuilder(blockSpawner, _fieldPositionsGenerator,
                    gameField, _pointTransform, _fieldBuilderInfo);
            });
        }
    }
}