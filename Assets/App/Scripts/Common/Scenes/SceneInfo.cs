using System;
using UnityEngine;

namespace Common.Scenes
{
    [Serializable]
    public class SceneInfo
    {
        [SerializeField] private string _key;
        [SerializeField] private string _sceneName;

        public string Key => _key;
        public string SceneName => _sceneName;
    }
}