using Libs.Popups.Animations.Base;
using Libs.Popups.Controls.Base;
using Libs.Popups.ViewModels;
using Libs.Popups.ViewModels.Actions;
using Libs.Popups.ViewModels.Commands;

namespace Libs.Popups
{
    public abstract class ViewModelPopup<TViewModel> : Popup where TViewModel : IPopupViewModel
    {
        [PopupProperty]
        public TViewModel ViewModel { get; set; }
        
        protected abstract void SetupViewModel(TViewModel viewModel);
        
        protected override void OnBeforeShowing()
        {
            SetupViewModel(ViewModel);
            ExecutePopupCommand(ViewModel.ShowAction.BeforeActionCommand);
        }
        protected override void OnBeforeClosing() => ExecutePopupCommand(ViewModel.CloseAction.BeforeActionCommand);
        protected override void OnShowed() => ExecutePopupCommand(ViewModel.ShowAction.AfterActionCommand);
        protected override void OnClosed() => ExecutePopupCommand(ViewModel.CloseAction.AfterActionCommand);

        protected void BindToAction(ControlBase controlBase, IControlAction controlAction, bool isChangingView = true,
            bool resetControl = true)
        {
            if (resetControl)
            {
                controlBase.Reset(false);
            }
            controlBase.BindToAction(controlAction);
            controlAction.IsChangingView = isChangingView;
            controlAction.IsExecutingChanged += ControlActionOnIsExecutingChanged;
        }
        
        protected void BindToActionWithValue(ControlBase controlBase, IControlAction controlAction, 
            object value, bool isChangingView = true, bool resetControl = true)
        {
            if (resetControl)
            {
                controlBase.Reset(false);
            }
            controlBase.BindToActionWithValue(controlAction, value);
            controlAction.IsChangingView = isChangingView;
            controlAction.IsExecutingChanged += ControlActionOnIsExecutingChanged;
        }

        protected void UpdateControl(ControlBase controlBase) => controlBase.UpdateControl();

        protected void Unbind(IControlAction controlAction)
        {
            controlAction.IsExecutingChanged -= ControlActionOnIsExecutingChanged;
        }

        private void ControlActionOnIsExecutingChanged(IControlAction controlAction, bool isExecuting)
        {
            if (isExecuting)
            {
                DisableInput();
            }

            if (isExecuting == false && controlAction.IsChangingView == false)
            {
                EnableInput();
            }
        }

        protected void SetAnimation(IAction action, IPopupAnimation popupAnimation) => action.SetAnimation(popupAnimation);
        protected override IPopupAnimation CreateCustomAppearAnimation() => ViewModel.ShowAction.PopupAnimation;
        protected override IPopupAnimation CreateCustomDisappearAnimation() => ViewModel.CloseAction.PopupAnimation;
        private static void ExecutePopupCommand(ICommand command) => command.Execute(null);
    }
}