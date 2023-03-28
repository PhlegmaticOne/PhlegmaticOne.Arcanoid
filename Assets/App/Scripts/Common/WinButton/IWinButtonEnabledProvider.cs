namespace Common.WinButton
{
    public interface IWinButtonEnabledProvider
    {
        void ChangeEnabled(bool enabled);
        bool IsEnabled { get; }
    }
}