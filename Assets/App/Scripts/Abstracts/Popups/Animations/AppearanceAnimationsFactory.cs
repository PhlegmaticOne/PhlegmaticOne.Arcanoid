using System;
using System.Collections.Generic;
using Abstracts.Popups.Animations.Base;
using Abstracts.Popups.Animations.Concrete;
using Abstracts.Popups.Animations.Types;
using UnityEngine;

namespace Abstracts.Popups.Animations
{
    public class AppearanceAnimationsFactory : IPopupAnimationsFactory<AppearAnimationType>
    {
        private const int Margin = 10;
        private readonly Dictionary<AppearAnimationType, Func<RectTransform, IPopupAnimation>> _animationFactories;

        public AppearanceAnimationsFactory()
        {
            _animationFactories = new Dictionary<AppearAnimationType, Func<RectTransform, IPopupAnimation>>
            {
                { AppearAnimationType.None, t => new NoneAnimation() },
                { AppearAnimationType.FadeIn, t => new PopupFadeAnimation(true) },
                { AppearAnimationType.FromBottom, t => 
                    PopupMoveAnimation.ToZeroFrom(new Vector3(0, -t.rect.height - Margin)) },
                { AppearAnimationType.FromTop, t => 
                    PopupMoveAnimation.ToZeroFrom(new Vector3(0, t.rect.height + Margin)) },
                { AppearAnimationType.FromLeftSide, t => 
                    PopupMoveAnimation.ToZeroFrom(new Vector3(-t.rect.width - Margin, 0)) },
                { AppearAnimationType.FromRightSide, t => 
                    PopupMoveAnimation.ToZeroFrom(new Vector3(t.rect.width + Margin, 0)) },
            };
        }
        
        public IPopupAnimation CreateAnimation(AppearAnimationType appearAnimationType, RectTransform mainCanvasTransform) => 
            _animationFactories[appearAnimationType].Invoke(mainCanvasTransform);
    }
}