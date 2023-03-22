using System;
using Libs.Popups.Animations.Base;

namespace Libs.Popups.ViewModels.Actions
{
    public interface IControlAction : IAction
    {
        event Action<bool> IsExecutingChanged;
        void Execute(object parameter);
        bool CanExecute(object parameter);
    }

    public interface IAction
    {
        void SetAnimation(IPopupAnimation popupAnimation);
    }
}