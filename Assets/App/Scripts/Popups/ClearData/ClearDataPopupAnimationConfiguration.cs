using Libs.Popups.Animations.Attributes;
using Libs.Popups.Animations.Info;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Popups.ClearData
{
    [CreateAssetMenu(menuName = "Popups/Animations/Create final accept popup animation configuration")]
    public class ClearDataPopupAnimationConfiguration : ScriptableObject
    {
        [Title(AnimationTitleNames.ShowTitle)]
        [SerializeField] private TweenAnimationInfo _showAnimation;
        
        [Title(AnimationTitleNames.CloseTitle)]
        [SerializeField] private TweenAnimationInfo _closeAnimation;

        public TweenAnimationInfo ShowAnimation => _showAnimation;
        public TweenAnimationInfo CloseAnimation => _closeAnimation;
    }
}