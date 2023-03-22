namespace Libs.Popups.ViewModels.Commands
{
    public abstract class EmptyCommandBase : ICommand
    {
        public bool CanExecute(object parameter) => CanExecute();

        public void Execute(object parameter) => Execute();

        protected virtual bool CanExecute() => true;
        protected abstract void Execute();
    }
}