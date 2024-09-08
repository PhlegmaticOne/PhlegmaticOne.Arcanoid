using Common.Energy;
using Common.Packs.Data.Repositories.Base;
using Common.Game.Providers.Providers;
using Common.Packs.Views.Commands;
using Common.Scenes;
using Libs.Popups.Base;
using Libs.Popups.ViewModels.Actions;
using Libs.Services;
using Popups.Common;
using Popups.Start;
using UnityEngine;

namespace Common.Packs.Views
{
    public class PackChoosePopupViewModelInstaller : ServiceInstaller
    {
        [SerializeField] private string _reasonPhraseKey;
        
        public override void InstallServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton(x =>
            {
                var popupManager = x.GetRequiredService<IPopupManager>();
                var energyManager = x.GetRequiredService<EnergyManager>();
                var gameDataProvider = x.GetRequiredService<IGameDataProvider>();
                var packRepository = x.GetRequiredService<IPackRepository>();
                var sceneChanger = x.GetRequiredService<ISceneChanger>();
                
                var spawnStartPopupCommand = new SpawnPopupCommand<StartPopup>(popupManager);
                var backControlCommand = new ChangeOnCloseControlCommand(popupManager, spawnStartPopupCommand);
                var packClickedCommand = new PackClickedCommand(energyManager, gameDataProvider, sceneChanger, packRepository);
                var packClickedCantExecuteHandler = new ShowEnergyPopupCommand(popupManager, _reasonPhraseKey);

                return new PackChoosePopupViewModel
                {
                    CloseAction = PopupAction.Empty,
                    ShowAction = PopupAction.Empty,
                    BackControlAction = new ControlAction(backControlCommand),
                    PackClickedAction = new ControlAction(packClickedCommand, packClickedCantExecuteHandler)
                };
            });
        }
    }
}