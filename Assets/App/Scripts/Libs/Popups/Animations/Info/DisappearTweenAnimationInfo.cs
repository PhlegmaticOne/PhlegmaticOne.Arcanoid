using System;
using Libs.Popups.Animations.Info.Types;
using UnityEngine;

namespace Libs.Popups.Animations.Info
{
    [Serializable]
    public class DisappearTweenAnimationInfo : TweenAnimationInfo
    {
        [SerializeField] private DisappearType _disappearType;
        public DisappearType DisappearType => _disappearType;
    }
}