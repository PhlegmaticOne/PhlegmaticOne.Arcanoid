using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Abstracts.Popups.Initialization
{
    public class PopupInitializersBuilder : IPopupInitializersBuilder
    {
        private readonly Dictionary<Type, Action<Popup>> _initializers;

        public PopupInitializersBuilder() => _initializers = new Dictionary<Type, Action<Popup>>();

        public void SetInitializerFor<TPopup>(UnityAction<TPopup> initializer) where TPopup : Popup => 
            _initializers.Add(typeof(TPopup), popup => initializer?.Invoke((TPopup)popup));

        public IPopupInitializersProvider BuildPopupInitializersProvider() => new PopupInitializersProvider(_initializers);
    }
}