using Libs.Popups.Animations.Animators;
using Libs.Popups.Animations.Base;
using Libs.Popups.Animations.Concrete;
using UnityEngine;
using UnityEngine.UI;

namespace Libs.Popups.Animations
{
    public static class Animate
    {
        public static IRectTransformAnimator RectTransform(RectTransform rectTransform) => 
            new RectTransformAnimator(rectTransform);
        public static TransformAnimator Transform(Transform transform) => 
            new TransformAnimator(transform);
        public static CanvasGroupAnimator CanvasGroup(CanvasGroup canvasGroup) => 
            new CanvasGroupAnimator(canvasGroup);

        public static ImageAnimator Image(Image image) => new ImageAnimator(image);
        public static IPopupAnimation None() => new NoneAnimation();
    }
}