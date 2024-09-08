namespace Libs.Popups.ViewModels.Commands
{
    public abstract class ParameterCommandBase<TParameter> : ICommand
    {
        public bool CanExecute(object parameter = null)
        {
            if (parameter is TParameter generic)
            {
                return CanExecute(generic);
            }

            return false;
        }

        public void Execute(object parameter = null)
        {
            if (parameter is TParameter generic)
            {
                Execute(generic);
            }
        }

        protected virtual bool CanExecute(TParameter parameter) => true;
        protected abstract void Execute(TParameter parameter);
    }
}