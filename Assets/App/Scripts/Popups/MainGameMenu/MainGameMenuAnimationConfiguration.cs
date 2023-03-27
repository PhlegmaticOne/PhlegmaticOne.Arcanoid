using Libs.Popups.Animations.Attributes;
using Libs.Popups.Animations.Info;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Popups.MainGameMenu
{
    [CreateAssetMenu(menuName = "Popups/Animations/Create main game menu popup animation configuration")]
    public class MainGameMenuAnimationConfiguration : ScriptableObject
    {
        [Title(AnimationTitleNames.ShowTitle)] [SerializeField]
        private TweenAnimationInfo _fadeInAnimation;

        [SerializeField] private AppearTweenAnimationInfo _showAnimation;

        [Title(AnimationTitleNames.CloseTitle)] [SerializeField]
        private TweenAnimationInfo _fadeOutAnimation;

        [SerializeField] private DisappearTweenAnimationInfo _closeAnimation;

        [Title("Energy animation")] [SerializeField] [Range(0f, 2f)]
        private float _energyAnimationTime;

        public TweenAnimationInfo FadeInAnimation => _fadeInAnimation;
        public AppearTweenAnimationInfo ShowAnimation => _showAnimation;
        public DisappearTweenAnimationInfo CloseAnimation => _closeAnimation;
        public TweenAnimationInfo FadeOutAnimation => _fadeOutAnimation;
        public float EnergyAnimationTime => _energyAnimationTime;
    }
}