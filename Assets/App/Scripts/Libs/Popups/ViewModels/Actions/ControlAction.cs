﻿using System;
using Libs.Popups.Animations.Base;
using Libs.Popups.ViewModels.Commands;

namespace Libs.Popups.ViewModels.Actions
{
    public class ControlAction : IControlAction
    {
        private readonly ICommand _command;
        private readonly ICommand _cantExecuteHandler;
        private IPopupAnimation _popupAnimation;
        private object _parameter;

        public static IControlAction Empty => new ControlAction(NoneCommand.New); 

        public ControlAction(ICommand command, ICommand cantExecuteHandler)
        {
            _command = command;
            _cantExecuteHandler = cantExecuteHandler;
        }
        
        public ControlAction(ICommand command) : this(command, null)
        {
            _command = command;
        }

        public bool IsChangingView { get; set; }
        public event Action<IControlAction, bool> IsExecutingChanged;
        public void Execute(object parameter)
        {
            if (_command.CanExecute(parameter) == false)
            {
                _cantExecuteHandler?.Execute(parameter);
                return;
            }

            _parameter = parameter;
            _popupAnimation.AnimationPlayed += PopupAnimationOnAnimationPlayed;
            SetIsExecuting(true);
            _popupAnimation.Play();
        }

        public bool CanExecute(object parameter) => _command.CanExecute(parameter);

        private void PopupAnimationOnAnimationPlayed()
        {
            _command.Execute(_parameter);
            SetIsExecuting(false);
            _popupAnimation.AnimationPlayed -= PopupAnimationOnAnimationPlayed;
            _popupAnimation.Stop();
            _parameter = null;
        }

        public void SetAnimation(IPopupAnimation popupAnimation)
        {
            _popupAnimation = null;
            _popupAnimation = popupAnimation;
        }

        private void SetIsExecuting(bool isExecuting) => IsExecutingChanged?.Invoke(this, isExecuting);
    }
}