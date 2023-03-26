using System.Collections.Generic;
using System.Linq;

namespace Common.Scenes
{
    public class ScenesProvider : IScenesProvider
    {
        private readonly List<SceneInfo> _sceneInfos;
        public ScenesProvider(IEnumerable<SceneInfo> sceneInfos) => 
            _sceneInfos = sceneInfos.ToList();

        public SceneInfo GetSceneByCustomKey(string key) => 
            _sceneInfos.First(x => x.Key == key);

        public SceneInfo GetSceneBySceneName(string sceneName) => 
            _sceneInfos.First(x => x.Scene.name == sceneName);
    }
}