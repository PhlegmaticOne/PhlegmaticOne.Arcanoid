using Libs.Popups.Animations.Attributes;
using Libs.Popups.Animations.Info;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Popups.Win
{
    [CreateAssetMenu(menuName = "Popups/Animations/Create win popup animation configuration")]
    public class WinPopupAnimationConfiguration : ScriptableObject
    {
        [Title(AnimationTitleNames.ShowTitle)]
        [SerializeField] private TweenAnimationInfo _fadeInAnimation;
        [SerializeField] private AppearTweenAnimationInfo _showAnimation;
        [SerializeField] private TweenAnimationInfo _packViewScaleAnimation;
        [SerializeField] private AppearTweenAnimationInfo _buttonsAppearAnimation;
        [SerializeField] private TweenAnimationInfo _youPassedAllPacksTextAppearAnimation;
        [SerializeField] private TweenAnimationInfo _changePackColorAnimation;
        [SerializeField] private float _energyAnimationTime;

        [Title(AnimationTitleNames.CloseTitle)] 
        [SerializeField] private DisappearTweenAnimationInfo _closeAnimation;
        [SerializeField] private TweenAnimationInfo _fadeOutAnimation;

        [Title("Continuous animations")] 
        [SerializeField] private TweenAnimationInfo _lightsAnimation;

        public TweenAnimationInfo FadeInAnimation => _fadeInAnimation;
        public AppearTweenAnimationInfo ShowAnimation => _showAnimation;
        public TweenAnimationInfo PackViewScaleAnimation => _packViewScaleAnimation;
        public AppearTweenAnimationInfo ButtonsAppearAnimation => _buttonsAppearAnimation;
        public TweenAnimationInfo YouPassedAllPacksTextAppearAnimation => _youPassedAllPacksTextAppearAnimation;
        public TweenAnimationInfo ChangePackColorAnimation => _changePackColorAnimation;
        public float EnergyAnimationTime => _energyAnimationTime;
        public DisappearTweenAnimationInfo CloseAnimation => _closeAnimation;
        public TweenAnimationInfo FadeOutAnimation => _fadeOutAnimation;
        public TweenAnimationInfo LightAnimation => _lightsAnimation;
    }
}