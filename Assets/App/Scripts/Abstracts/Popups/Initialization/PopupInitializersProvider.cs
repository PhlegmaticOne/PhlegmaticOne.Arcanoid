using System;
using System.Collections.Generic;

namespace Abstracts.Popups.Initialization
{
    public class PopupInitializersProvider : IPopupInitializersProvider
    {
        private readonly Dictionary<Type, Action<Popup>> _initializers;
        
        public PopupInitializersProvider(Dictionary<Type, Action<Popup>> initializers) => _initializers = initializers;

        public void InitializePopup(Popup popup)
        {
            var initializer = _initializers[popup.GetType()];
            initializer(popup);
        }
    }
}