using Libs.Popups.Animations.Attributes;
using Libs.Popups.Animations.Info;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Popups.Lose
{
    [CreateAssetMenu(menuName = "Popups/Animations/Create lose popup animation configuration")]
    public class LosePopupAnimationConfiguration : ScriptableObject
    {
        [Title(AnimationTitleNames.ShowTitle)] [SerializeField] 
        private TweenAnimationInfo _fadeInAnimation;

        [SerializeField] private AppearTweenAnimationInfo _showAnimation;

        [Title(AnimationTitleNames.CloseTitle)] [SerializeField]
        private DisappearTweenAnimationInfo _closeAnimation;

        [SerializeField] private TweenAnimationInfo _fadeOutAnimation;

        [Title("Energy animation")] [SerializeField] [Range(0f, 2f)]
        private float _energyAnimationTime;

        public TweenAnimationInfo FadeInAnimation => _fadeInAnimation;
        public AppearTweenAnimationInfo ShowAnimation => _showAnimation;
        public DisappearTweenAnimationInfo CloseAnimation => _closeAnimation;
        public TweenAnimationInfo FadeOutAnimation => _fadeOutAnimation;
        public float EnergyAnimationTime => _energyAnimationTime;
    }
}