namespace Libs.Localization.Models
{
    public class LocaleInfo
    {
        public LocaleInfo(string systemName) => SystemName = systemName;
        
        public LocaleInfo(string systemName, string displayName) : this(systemName) => DisplayName = displayName;
        public string DisplayName { get; private set; }
        public string SystemName { get; }
        public void SetDisplayName(string displayName) => DisplayName = displayName;
    }
}