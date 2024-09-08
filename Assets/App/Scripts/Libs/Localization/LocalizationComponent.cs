using System.Collections.Generic;
using Libs.Localization.Base;
using Libs.Localization.Components.Base;
using Libs.Localization.Context;
using UnityEngine;

namespace Libs.Localization
{
    public class LocalizationComponent : MonoBehaviour, ILocalizable
    {
        [SerializeField] private List<LocalizationBindableComponent> _staticLocalizedComponents;
        private LocalizationContext _localizationContext;
        public IEnumerable<ILocalizationBindable> GetBindableComponents() => _staticLocalizedComponents;

        public void BindInitial(ILocalizationManager localizationManager)
        {
            _localizationContext = LocalizationContext.Create(localizationManager);
            AddNew(this);
        }
        
        public void AddNew(ILocalizable localizable)
        {
            _localizationContext.BindLocalizable(localizable);
        }

        public void Refresh() => _localizationContext.Refresh();
        
        public void Unbind()
        {
            _localizationContext.Flush();
            _localizationContext = null;
        }

    }
}