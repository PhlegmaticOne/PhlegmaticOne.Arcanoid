using System;
using Libs.Popups.Animations.Types;
using UnityEngine;

namespace Libs.Popups.Configurations
{
    [Serializable]
    public class PopupConfiguration
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