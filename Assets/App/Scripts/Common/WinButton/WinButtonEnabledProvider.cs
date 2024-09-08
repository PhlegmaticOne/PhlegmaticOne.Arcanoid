using UnityEngine;

namespace Common.WinButton
{
    public class WinButtonEnabledProvider : IWinButtonEnabledProvider
    {
        private const string Key = "WinButtonEnabled";

        public void ChangeEnabled(bool enabled) => PlayerPrefs.SetInt(Key, GetInt(enabled));

        public bool IsEnabled => PlayerPrefs.GetInt(Key) == 1;
        private static int GetInt(bool value) => value ? 1 : 0;
    }
}