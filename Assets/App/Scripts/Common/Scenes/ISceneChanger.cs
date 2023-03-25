using System;

namespace Common.Scenes
{
    public interface ISceneChanger
    {
        void ChangeScene(int sceneIndex);
        event Action SceneChanged;
        event Action OnOverlay;
    }
}