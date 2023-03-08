using UnityEngine;
using UnityEngine.SceneManagement;

namespace Libs.Popups
{
    public class PopupContainer : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        private static bool _initialized;
        private void Awake()
        {
            if (_initialized)
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
            _initialized = true;
        }

        private void Start() => SceneManager.sceneLoaded += SceneManagerOnsceneLoaded;
        private void SceneManagerOnsceneLoaded(Scene scene, LoadSceneMode loadSceneMode) => _canvas.worldCamera = Camera.main;
        private void OnDisable() => SceneManager.sceneLoaded -= SceneManagerOnsceneLoaded;
    }
}