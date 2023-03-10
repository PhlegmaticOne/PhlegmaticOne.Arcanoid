using Common.Data.Models;
using Game.Accessors;
using Game.Field;
using Game.PlayerObjects.BallObject;
using Game.PlayerObjects.ShipObject;
using Libs.Services;

namespace Common.ServiceInstallers
{
    public class ObjectAccessorsInstaller : ServiceInstaller
    {
        public override void InstallServices(IServiceCollection serviceCollection)
        {
            AddAccessor<GameData>(serviceCollection);
            AddAccessor<GameField>(serviceCollection);
            AddAccessor<Ship>(serviceCollection);
            AddAccessor<BallsOnField>(serviceCollection);
            AddAccessor<LevelData>(serviceCollection);
        }

        private static void AddAccessor<T>(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IObjectAccessor<T>>(new ObjectAccessor<T>());
        }
    }
}