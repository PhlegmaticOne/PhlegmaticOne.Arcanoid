using Libs.Popups.Animations.Base;

namespace Libs.Popups.Animations.Concrete
{
    public class NoneAnimation : PopupAnimationBase
    {
        public override void Play() => OnAnimationPlayed();
        public override void Stop() { }
    }
}