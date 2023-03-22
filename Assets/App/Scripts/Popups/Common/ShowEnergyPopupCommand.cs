using Libs.Popups.Base;
using Libs.Popups.ViewModels.Commands;
using Popups.Energy;

namespace Popups.Common
{
    public class ShowEnergyPopupCommand : ICommand
    {
        private readonly IPopupManager _popupManager;
        private readonly string _reasonPhraseKey;

        public ShowEnergyPopupCommand(IPopupManager popupManager, string reasonPhraseKey)
        {
            _popupManager = popupManager;
            _reasonPhraseKey = reasonPhraseKey;
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            var popup = _popupManager.SpawnPopup<EnergyPopup>();
            popup.ShowWithReasonPhraseKey(_reasonPhraseKey);
        }
    }
}