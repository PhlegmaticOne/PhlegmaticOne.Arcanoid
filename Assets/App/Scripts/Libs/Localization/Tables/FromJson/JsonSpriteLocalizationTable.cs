using System.Collections.Generic;
using Libs.Localization.Tables.FromJson.Base;
using UnityEngine;

namespace Libs.Localization.Tables.FromJson
{
    public class JsonSpriteLocalizationTable : JsonLocalizationTableBase<Dictionary<string, string>, Sprite>
    {
        public JsonSpriteLocalizationTable(string json) : base(json) { }
        
        protected override Sprite GetLocalizedValue(string key)
        {
            if (DeserializedObject.TryGetValue(key, out var value))
            {
                var spritePath = value;
                var sprite = Resources.Load<Sprite>(spritePath);
                return sprite;
            }

            return null;
        }
    }
}