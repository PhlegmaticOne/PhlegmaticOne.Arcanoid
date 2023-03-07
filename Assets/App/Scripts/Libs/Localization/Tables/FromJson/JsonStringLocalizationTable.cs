using System.Collections.Generic;
using Libs.Localization.Tables.FromJson.Base;

namespace Libs.Localization.Tables.FromJson
{
    public class JsonStringLocalizationTable : JsonLocalizationTableBase<Dictionary<string, string>, string>
    {
        public JsonStringLocalizationTable(string json) : base(json) { }
        
        protected override string GetLocalizedValue(string key) => 
            DeserializedObject.TryGetValue(key, out var value) ? value : null;
    }
}