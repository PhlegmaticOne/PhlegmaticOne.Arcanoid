using System;
using UnityEngine;

namespace Abstracts.Popups.Animations.Base
{
    public interface IPopupAnimationsFactory<in T> where T : Enum
    {
        IPopupAnimation CreateAnimation(T animationType, RectTransform mainCanvasTransform);
    }
}