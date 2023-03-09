using UnityEngine;
using UnityEngine.SceneManagement;

namespace Libs.Popups
{
    public class PopupContainer : MonoBehaviour
    {
        private static bool _initialized;
        
        [SerializeField] private Canvas _canvas;
        public RectTransform CanvasTransform => _canvas.transform as RectTransform;
        
        private void Awake()
        {
            if (_initialized)
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
            SetCamera();
            _initialized = true;
        }

        private void Start() => SceneManager.sceneLoaded += SceneManagerOnsceneLoaded;
        private void SceneManagerOnsceneLoaded(Scene scene, LoadSceneMode loadSceneMode) => SetCamera();
        private void OnDisable() => SceneManager.sceneLoaded -= SceneManagerOnsceneLoaded;
        private void SetCamera() => _canvas.worldCamera = Camera.main;
    }
}