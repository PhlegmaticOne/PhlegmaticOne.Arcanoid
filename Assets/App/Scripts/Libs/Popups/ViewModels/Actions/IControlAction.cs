using System;
using Libs.Popups.Animations.Base;

namespace Libs.Popups.ViewModels.Actions
{
    public interface IControlAction : IAction
    {
        public bool IsChangingView { get; set; }
        event Action<IControlAction, bool> IsExecutingChanged;
        void Execute(object parameter);
        bool CanExecute(object parameter);
    }

    public interface IAction
    {
        void SetAnimation(IPopupAnimation popupAnimation);
    }
}