using System.Collections.Generic;
using Libs.Localization.Tables.FromJson.Base;
using UnityEditor;
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
                var sprite = AssetDatabase.LoadAssetAtPath<Sprite>(spritePath);
                return sprite;
            }

            return null;
        }
    }
}