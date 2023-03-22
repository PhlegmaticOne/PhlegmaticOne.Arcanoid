using System;
using DG.Tweening;
using UnityEngine;

namespace Libs.Popups.Animations.Info
{
    [Serializable]
    public class TweenAnimationInfo
    {
        [SerializeField] [Range(0f, 3f)] private float _animationTime;
        [SerializeField] private Ease _ease;

        public float AnimationTime => _animationTime;
        public Ease Ease => _ease;
    }
}