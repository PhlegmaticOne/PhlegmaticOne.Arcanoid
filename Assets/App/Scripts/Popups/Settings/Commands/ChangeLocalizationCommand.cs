using System.Linq;
using Common.Localization;
using Libs.Localization.Base;
using Libs.Popups.ViewModels.Commands;

namespace Popups.Settings.Commands
{
    public class ChangeLocalizationCommand : ParameterCommandBase<string>
    {
        private readonly ILocalizationManager _localizationManager;
        private readonly ILocalizationProvider _localizationProvider;

        public ChangeLocalizationCommand(ILocalizationManager localizationManager,
            ILocalizationProvider localizationProvider)
        {
            _localizationManager = localizationManager;
            _localizationProvider = localizationProvider;
        }
        
        protected override void Execute(string parameter)
        {
            if (_localizationManager.CurrentLocale.DisplayName != parameter)
            {
                var locale = _localizationManager.GetAvailableLocales().First(x => x.DisplayName == parameter);
                _localizationManager.SetLocale(locale);
                _localizationProvider.SaveLocale(locale);
            }
        }
    }
}