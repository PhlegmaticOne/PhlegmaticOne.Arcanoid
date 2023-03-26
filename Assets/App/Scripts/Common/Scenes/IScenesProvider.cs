namespace Common.Scenes
{
    public interface IScenesProvider
    {
        SceneInfo GetSceneByCustomKey(string key);
        SceneInfo GetSceneBySceneName(string sceneName);
    }
}