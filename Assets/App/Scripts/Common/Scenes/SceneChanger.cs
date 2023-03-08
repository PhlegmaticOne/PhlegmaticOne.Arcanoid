using Libs.Popups;
using Libs.Popups.Base;
using UnityEngine.SceneManagement;

namespace Common.Scenes
{
    public class SceneChanger<T> where T : Popup
    {
        private readonly IPopupManager _popupManager;

        public SceneChanger(IPopupManager popupManager)
        {
            _popupManager = popupManager;
            SceneManager.sceneLoaded += SceneManagerOnsceneLoaded;
        }

        public void ChangeScene(int sceneId) => SceneManager.LoadScene(sceneId);

        private void SceneManagerOnsceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            _popupManager.SpawnPopup<T>();
            SceneManager.sceneLoaded -= SceneManagerOnsceneLoaded;
        }
    }
}