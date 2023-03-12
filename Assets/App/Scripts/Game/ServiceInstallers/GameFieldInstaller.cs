using Game.Field;
using Libs.Services;
using UnityEngine;

namespace Game.ServiceInstallers
{
    public class GameFieldInstaller : ServiceInstaller
    {
        [SerializeField] private GameField _gameField;
        public override void InstallServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton(_gameField);
        }
    }
}