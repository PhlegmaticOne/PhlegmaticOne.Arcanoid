using System;
using Libs.Popups.Animations.Types;
using UnityEngine;

namespace Libs.Popups.Configurations
{
    [CreateAssetMenu(menuName = "Popups/Create popup configuration", order = 0)]
    public class PopupConfiguration : ScriptableObject
    {
        [SerializeField] private string _sortingLayerName;
        [SerializeField] private PopupAnimationConfiguration _popupAnimationConfiguration;
        [SerializeField] private Popup _popup;

        public string SortingLayerName => _sortingLayerName;
        public Popup Popup => _popup;
        public PopupAnimationConfiguration PopupAnimationConfiguration => _popupAnimationConfiguration;
    }

    [Serializable]
    public class PopupAnimationConfiguration
    {
        [SerializeField] private AppearAnimationType _appearAnimationType;
        [SerializeField] private float _appearanceTime;
        [SerializeField] private DisappearAnimationType _disappearAnimationType;
        [SerializeField] private float _disappearanceTime;
        
        public AppearAnimationType AppearAnimationType => _appearAnimationType;
        public float AppearanceTime => _appearanceTime;
        public DisappearAnimationType DisappearAnimationType => _disappearAnimationType;
        public float DisappearanceTime => _disappearanceTime;
    }
}