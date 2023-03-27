using DG.Tweening;
using Libs.Popups;
using Libs.Popups.Animations;
using Libs.Popups.Animations.Base;
using Libs.Popups.Animations.Extensions;
using Libs.Popups.Animations.Info;
using UnityEngine;
using UnityEngine.UI;

namespace Popups.Transition
{
    public class SceneTransitionPopup : Popup
    {
        [SerializeField] private SceneTransitionPopupAnimationConfiguration _animationConfiguration;
        [SerializeField] private Image _loadingImage;

        private Tween _rotateTween;

        protected override IPopupAnimation CreateCustomAppearAnimation()
        {
            return Animate.RectTransform(RectTransform)
                .RelativeTo(ParentTransform)
                .Appear(_animationConfiguration.ShowAnimation)
                .ToPopupCallbackAnimation();
        }

        protected override IPopupAnimation CreateCustomDisappearAnimation()
        {
            return Animate.RectTransform(RectTransform)
                .RelativeTo(ParentTransform)
                .Disappear(_animationConfiguration.CloseAnimation)
                .ToPopupCallbackAnimation();
        }

        protected override void OnBeforeShowing()
        {
            _rotateTween = Animate
                .Transform(_loadingImage.transform)
                .FullCircleAnimate(_animationConfiguration.LoadingAnimation)
                .Play();
        }
        
        public override void Reset()
        {
            _rotateTween.Kill();
            _loadingImage.transform.rotation = Quaternion.Euler(0, 0, 0);
            ToZeroPosition();
        }

        public override void EnableInput() { }

        public override void DisableInput() { }
    }
}