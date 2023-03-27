using DG.Tweening;
using Libs.Popups.Animations.Base;
using Libs.Popups.Animations.Concrete;

namespace Libs.Popups.Animations.Extensions
{
    public static class TweenExtensions
    {
        public static IPopupAnimation ToPopupCallbackAnimation(this Tween tween) => 
            new DoTweenCallbackAnimation(() => tween);
    }
}