using Libs.Popups.Animations.Attributes;
using Libs.Popups.Animations.Info;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Popups.Settings
{
    [CreateAssetMenu(menuName = "Popups/Animations/Create settings popup animation configuration")]
    public class SettingsPopupAnimationConfiguration : ScriptableObject
    {
        [Title(AnimationTitleNames.ShowTitle)] [SerializeField]
        private AppearTweenAnimationInfo _showAnimation;

        [Title(AnimationTitleNames.CloseTitle)] [SerializeField]
        private DisappearTweenAnimationInfo _closeAnimation;

        public AppearTweenAnimationInfo ShowAnimation => _showAnimation;
        public DisappearTweenAnimationInfo CloseAnimation => _closeAnimation;
    }
}