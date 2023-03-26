using System;

namespace Common.Scenes
{
    public interface ISceneChanger
    {
        SceneInfo CurrentScene { get; }
        void ChangeScene(string sceneKey);
        event Action SceneChanged;
        event Action OnOverlay;
    }
}