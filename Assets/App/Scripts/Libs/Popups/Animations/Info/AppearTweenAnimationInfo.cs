using System;
using Libs.Popups.Animations.Info.Types;
using UnityEngine;

namespace Libs.Popups.Animations.Info
{
    [Serializable]
    public class AppearTweenAnimationInfo : TweenAnimationInfo
    {
        [SerializeField] private AppearType _appearType;
        public AppearType AppearType => _appearType;
    }
}