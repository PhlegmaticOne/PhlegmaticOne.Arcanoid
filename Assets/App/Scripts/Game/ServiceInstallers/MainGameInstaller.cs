using Game.Base;
using Game.Composites;
using Game.Field.Builder;
using Game.GameEntities.PlayerObjects.BallObject.Spawners;
using Game.ObjectParticles;
using Libs.InputSystem;
using Libs.Pooling.Base;
using Libs.Services;
using Libs.TimeActions;
using UnityEngine;

namespace Game.ServiceInstallers
{
    public class MainGameInstaller : ServiceInstaller
    {
        [SerializeField] private float _slowDownTime;
        [SerializeField] private WinParticles _winParticles;
        public override void InstallServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IGame<MainGameData, MainGameEvents>>(x =>
            {
                var global = ServiceProviderAccessor.Global;

                var gameSystems = x.GetRequiredService<GameSystems>();
                var entitiesOnField = x.GetRequiredService<EntitiesOnFieldCollection>();
                
                var ballSpawner = x.GetRequiredService<IBallSpawner>();
                var fieldBuilder = x.GetRequiredService<IFieldBuilder>();
                var inputSystem = x.GetRequiredService<IInputSystem>();
                var poolProvider = global.GetRequiredService<IPoolProvider>();
                var timeActionsManager = x.GetRequiredService<TimeActionsManager>();
                var particleManager = x.GetRequiredService<ParticleManager>();
                
                gameSystems.ControlSystem.Initialize(inputSystem, entitiesOnField.Ship);
                var game = new MainGame(poolProvider,
                    entitiesOnField,
                    gameSystems,
                    _winParticles,
                    fieldBuilder,
                    timeActionsManager,
                    particleManager,
                    ballSpawner);
                game.SetParameters(_slowDownTime);
                return game;
            });
        }
    }
}