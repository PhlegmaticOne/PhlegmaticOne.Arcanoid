using Common.Energy.Repositories;
using Common.Packs.Data.Repositories.Base;
using Libs.Popups.Base;
using Libs.Popups.ViewModels.Commands;

namespace Popups.Settings.Commands
{
    public class ClearPlayerDataCommand : EmptyCommandBase
    {
        private readonly IPackRepository _packRepository;
        private readonly IEnergyRepository _energyRepository;
        private readonly IPopupManager _popupManager;

        public ClearPlayerDataCommand(IPackRepository packRepository, IEnergyRepository energyRepository,
            IPopupManager popupManager)
        {
            _packRepository = packRepository;
            _energyRepository = energyRepository;
            _popupManager = popupManager;
        }
        
        protected override void Execute()
        {
            _packRepository.Clear();
            _energyRepository.Clear();
            _popupManager.CloseLastPopup();
        }
    }
}