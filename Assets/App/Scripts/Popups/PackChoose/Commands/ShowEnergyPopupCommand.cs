using Libs.Popups.Base;
using Libs.Popups.ViewModels.Commands;
using Popups.Energy;

namespace Popups.Common
{
    public class ShowEnergyPopupCommand : EmptyCommandBase
    {
        private readonly IPopupManager _popupManager;
        private readonly string _reasonPhraseKey;

        public ShowEnergyPopupCommand(IPopupManager popupManager, string reasonPhraseKey)
        {
            _popupManager = popupManager;
            _reasonPhraseKey = reasonPhraseKey;
        }

        protected override void Execute()
        {
            var popup = _popupManager.SpawnPopup<EnergyPopup>();
            popup.ShowWithReasonPhraseKey(_reasonPhraseKey);
        }
    }
}