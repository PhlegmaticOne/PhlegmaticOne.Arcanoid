namespace Libs.Popups.ViewModels.Commands
{
    public interface ICommand
    {
        bool CanExecute(object parameter);
        void Execute(object parameter);
    }
    
    public class NoneCommand : ICommand
    {
        public static ICommand New => new NoneCommand();
        public bool CanExecute(object parameter) => true;
        public void Execute(object parameter) { }
    }
}