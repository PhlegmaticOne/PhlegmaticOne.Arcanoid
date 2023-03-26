using System;
using UnityEditor;
using UnityEngine;

namespace Common.Scenes
{
    [Serializable]
    public class SceneInfo
    {
        [SerializeField] private string _key;
        [SerializeField] private SceneAsset _scene;

        public string Key => _key;
        public SceneAsset Scene => _scene;
    }
}