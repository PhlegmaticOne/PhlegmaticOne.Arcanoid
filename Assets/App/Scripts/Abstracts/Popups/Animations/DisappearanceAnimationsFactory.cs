using System;
using System.Collections.Generic;
using Abstracts.Popups.Animations.Base;
using Abstracts.Popups.Animations.Concrete;
using Abstracts.Popups.Animations.Types;
using UnityEngine;

namespace Abstracts.Popups.Animations
{
    public class DisappearanceAnimationsFactory : IPopupAnimationsFactory<DisappearAnimationType>
    {
        private const int Margin = 10;
        private readonly Dictionary<DisappearAnimationType, Func<RectTransform, IPopupAnimation>> _animationFactories;

        public DisappearanceAnimationsFactory()
        {
            _animationFactories = new Dictionary<DisappearAnimationType, Func<RectTransform, IPopupAnimation>>
            {
                { DisappearAnimationType.None, t => new NoneAnimation() },
                { DisappearAnimationType.FadeOut, t => new PopupFadeAnimation(false) },
                { DisappearAnimationType.ToBottom, t => 
                    PopupMoveAnimation.FromZeroTo(new Vector3(0, -t.rect.height - Margin)) },
                { DisappearAnimationType.ToTop, t => 
                    PopupMoveAnimation.FromZeroTo(new Vector3(0, t.rect.height + Margin)) },
                { DisappearAnimationType.ToLeftSide, t => 
                    PopupMoveAnimation.FromZeroTo(new Vector3(-t.rect.width - Margin, 0)) },
                { DisappearAnimationType.ToRightSide, t => 
                    PopupMoveAnimation.FromZeroTo(new Vector3(t.rect.width + Margin, 0)) },
            };
        }
        public IPopupAnimation CreateAnimation(DisappearAnimationType animationType, RectTransform mainCanvasTransform)
        {
            return _animationFactories[animationType].Invoke(mainCanvasTransform);
        }
    }
}