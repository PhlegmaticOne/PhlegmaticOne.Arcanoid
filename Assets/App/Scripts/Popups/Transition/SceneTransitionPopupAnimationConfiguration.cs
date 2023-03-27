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
        private AppearTweenAnimationInfo _showAnimation;

        [Title(AnimationTitleNames.CloseTitle)] [SerializeField]
        private DisappearTweenAnimationInfo _closeAnimation;

        [Title("Loading")] [SerializeField] 
        private TweenAnimationInfo _loadingAnimation;

        public AppearTweenAnimationInfo ShowAnimation => _showAnimation;
        public DisappearTweenAnimationInfo CloseAnimation => _closeAnimation;
        public TweenAnimationInfo LoadingAnimation => _loadingAnimation;
    }
}