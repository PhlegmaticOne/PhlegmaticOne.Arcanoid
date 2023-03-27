using Libs.Popups.Animations.Attributes;
using Libs.Popups.Animations.Info;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Popups.Start
{
    [CreateAssetMenu(menuName = "Popups/Animations/Create start popup animation configuration")]
    public class StartPopupAnimationConfiguration : ScriptableObject
    {
        [Title(AnimationTitleNames.ShowTitle)]
        [SerializeField] private TweenAnimationInfo _fadeInAnimation;
        [SerializeField] private AppearTweenAnimationInfo _buttonsAppearAnimation;

        [Title(AnimationTitleNames.CloseTitle)] 
        [SerializeField] private DisappearTweenAnimationInfo _closeAnimation;
        [SerializeField] private DisappearTweenAnimationInfo _buttonsDisappearAnimation;

        public TweenAnimationInfo FadeInAnimation => _fadeInAnimation;
        public AppearTweenAnimationInfo ButtonsAppearAnimation => _buttonsAppearAnimation;

        public DisappearTweenAnimationInfo CloseAnimation => _closeAnimation;
        public DisappearTweenAnimationInfo ButtonsDisappearAnimation => _buttonsDisappearAnimation;
    }
}