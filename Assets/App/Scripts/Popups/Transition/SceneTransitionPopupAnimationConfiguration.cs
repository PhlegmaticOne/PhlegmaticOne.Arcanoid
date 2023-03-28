using Libs.Popups.Animations.Attributes;
using Libs.Popups.Animations.Info;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Popups.Transition
{
    [CreateAssetMenu(menuName = "Popups/Animations/Create scene transition popup animation configuration")]
    public class SceneTransitionPopupAnimationConfiguration : ScriptableObject
    {
        [Title(AnimationTitleNames.ShowTitle)] [SerializeField]
        private TweenAnimationInfo _fadeInAnimation;

        [Title(AnimationTitleNames.CloseTitle)] [SerializeField]
        private TweenAnimationInfo _fadeOutAnimation;

        [Title("Loading")] [SerializeField] 
        private TweenAnimationInfo _loadingAnimation;

        public TweenAnimationInfo FadeInAnimation => _fadeInAnimation;
        public TweenAnimationInfo FadeOutAnimation => _fadeOutAnimation;
        public TweenAnimationInfo LoadingAnimation => _loadingAnimation;
    }
}