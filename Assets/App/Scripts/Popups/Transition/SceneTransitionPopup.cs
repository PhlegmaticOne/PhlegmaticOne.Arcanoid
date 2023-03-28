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
            return Animate.CanvasGroup(PopupView.CanvasGroup)
                .FadeIn(_animationConfiguration.FadeInAnimation)
                .ToPopupCallbackAnimation();
        }

        protected override IPopupAnimation CreateCustomDisappearAnimation()
        {
            return Animate.CanvasGroup(PopupView.CanvasGroup)
                .FadeOut(_animationConfiguration.FadeOutAnimation)
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