using System.Collections.Generic;

namespace Libs.Localization.Base
{
    public interface ILocalizable
    {
        IEnumerable<ILocalizationBindable> GetBindableComponents();
    }
}