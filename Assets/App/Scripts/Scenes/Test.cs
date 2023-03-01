using System.Collections;
using App.Scripts.Common.Localization;
using App.Scripts.Common.Localization.Base;
using UnityEngine;

namespace App.Scripts.Scenes
{
    public class Test : MonoBehaviour
    {
        [SerializeField] private LocalizationSelector _localizationSelector;

        private ILocalizationManager _localizationManager;
        
        private IEnumerator Start()
        {
            var localizationManager = new LocalizationManager(new[] { "UI" });
            yield return localizationManager.Initialize();
            _localizationSelector.Initialize(localizationManager);
            _localizationManager = localizationManager;
        }
    }
}