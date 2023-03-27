using Libs.Popups.Animations.Attributes;
using Libs.Popups.Animations.Info;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Popups.Energy
{
    [CreateAssetMenu(menuName = "Popups/Animations/Create energy popup animation configuration")]
    public class EnergyPopupAnimationConfiguration : ScriptableObject
    {
        [Title(AnimationTitleNames.ShowTitle)]
        [SerializeField] private AppearTweenAnimationInfo _showAnimationInfo;
        
        [Title(AnimationTitleNames.CloseTitle)]
        [SerializeField] private DisappearTweenAnimationInfo _closeAnimationInfo;

        public AppearTweenAnimationInfo ShowAnimation => _showAnimationInfo;
        public DisappearTweenAnimationInfo CloseAnimation => _closeAnimationInfo;
    }
}