using Common.Bag;
using Common.Energy;
using Common.Packs.Data.Repositories.Base;
using Common.Scenes;
using Game;
using Game.Base;
using Libs.Popups.Base;
using Libs.Services;
using Popups.Win.Factory;

namespace Popups.Win
{
    public class WinPopupViewModelInstaller : ServiceInstaller
    {
        public override void InstallServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IWinPopupViewModelFactory>(x =>
            {
                var global = ServiceProviderAccessor.Global;
                var game = x.GetRequiredService<IGame<MainGameData, MainGameEvents>>();
                var popupManager = global.GetRequiredService<IPopupManager>();
                var objectBag = global.GetRequiredService<IObjectBag>();
                var levelsRepository = global.GetRequiredService<ILevelRepository>();
                var packRepository = global.GetRequiredService<IPackRepository>();
                var energyManager = global.GetRequiredService<EnergyManager>();
                var sceneChanger = global.GetRequiredService<ISceneChanger>();

                return new WinPopupViewModelFactory(objectBag, game, popupManager, levelsRepository,
                    packRepository, energyManager, sceneChanger);
            });

            serviceCollection.AddTransient(x =>
            {
                var factory = x.GetRequiredService<IWinPopupViewModelFactory>();
                return factory.CreateWinPopupViewModel();
            });
        }
    }
}