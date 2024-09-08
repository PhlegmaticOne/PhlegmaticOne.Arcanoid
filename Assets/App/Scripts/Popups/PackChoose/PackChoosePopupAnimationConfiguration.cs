using Libs.Popups.Animations.Attributes;
using Libs.Popups.Animations.Info;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Common.Packs.Views
{
    [CreateAssetMenu(menuName = "Popups/Animations/Create pack choose popup animation configuration")]
    public class PackChoosePopupAnimationConfiguration : ScriptableObject
    {
        [Title(AnimationTitleNames.ShowTitle)] [SerializeField]
        private AppearTweenAnimationInfo _showAnimation;
        
        [Title(AnimationTitleNames.CloseTitle)] [SerializeField]
        private DisappearTweenAnimationInfo _closeAnimation;

        [Title("Pack click")]
        [SerializeField] private DisappearTweenAnimationInfo _packClickedAnimation;
        
        [Title("Energy animation")]
        [SerializeField] private float _energyAnimationTime;

        public AppearTweenAnimationInfo ShowAnimation => _showAnimation;
        public DisappearTweenAnimationInfo CloseAnimation => _closeAnimation;
        public DisappearTweenAnimationInfo PackClickedAnimation => _packClickedAnimation;
        public float EnergyAnimationTime => _energyAnimationTime;
    }
}