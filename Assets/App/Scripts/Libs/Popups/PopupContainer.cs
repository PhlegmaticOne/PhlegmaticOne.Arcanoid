using UnityEngine;
using UnityEngine.SceneManagement;

namespace Libs.Popups
{
    public class PopupContainer : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        public static RectTransform ParentTransform { get; private set; }
        
        private static bool _initialized;
        private void Awake()
        {
            if (_initialized)
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
            ParentTransform = _canvas.transform as RectTransform;
            _initialized = true;
        }

        private void Start() => SceneManager.sceneLoaded += SceneManagerOnsceneLoaded;
        private void SceneManagerOnsceneLoaded(Scene scene, LoadSceneMode loadSceneMode) => _canvas.worldCamera = Camera.main;
        private void OnDisable() => SceneManager.sceneLoaded -= SceneManagerOnsceneLoaded;
    }
}