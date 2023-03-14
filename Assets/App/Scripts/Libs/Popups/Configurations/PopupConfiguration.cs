using System;
using System.Reflection;
using Libs.Popups.Animations.Types;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Libs.Popups.Configurations
{
    [CreateAssetMenu(menuName = "Popups/Create popup configuration", order = 0)]
    public class PopupConfiguration : ScriptableObject
    {
        [SerializeField]
        // [ValueDropdown(nameof(GetSortingLayerNames))]
        private string _sortingLayerName;
        
        [SerializeField] private PopupAnimationConfiguration _popupAnimationConfiguration;
        [SerializeField] private Popup _popup;

        public string SortingLayerName => _sortingLayerName;
        public Popup Popup => _popup;
        public PopupAnimationConfiguration PopupAnimationConfiguration => _popupAnimationConfiguration;
        
        // public static string[] GetSortingLayerNames()
        // {
        //     var internalEditorUtilityType = typeof(InternalEditorUtility);
        //     var sortingLayersProperty = internalEditorUtilityType.GetProperty("sortingLayerNames",
        //         BindingFlags.Static | BindingFlags.NonPublic);
        //     return (string[])sortingLayersProperty.GetValue(null, new object[0]);
        // }
    }

    [Serializable]
    public class PopupAnimationConfiguration
    {
        [SerializeField] private bool _usesCustomAppearAnimation;
        
        [SerializeField] 
        [HideIf(nameof(_usesCustomAppearAnimation))]
        private AppearAnimationType _appearAnimationType;
        
        [SerializeField]
        [HideIf(nameof(_usesCustomAppearAnimation))]
        private float _appearanceTime;
        
        
        [SerializeField] private bool _usesCustomDisappearAnimation;
        
        [SerializeField] 
        [HideIf(nameof(_usesCustomDisappearAnimation))]
        private DisappearAnimationType _disappearAnimationType;
        
        [HideIf(nameof(_usesCustomDisappearAnimation))]
        [SerializeField] private float _disappearanceTime;
        
        public AppearAnimationType AppearAnimationType => _appearAnimationType;
        public float AppearanceTime => _appearanceTime;
        public DisappearAnimationType DisappearAnimationType => _disappearAnimationType;
        public float DisappearanceTime => _disappearanceTime;
        public bool UsesCustomAppearAnimation => _usesCustomAppearAnimation;
        public bool UsesCustomDisappearAnimation => _usesCustomDisappearAnimation;
    }
}