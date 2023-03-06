using UnityEngine;
using UnityEngine.SceneManagement;

namespace Libs.Popups
{
    public class PopupContainer : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        private void Awake() => DontDestroyOnLoad(gameObject);
        private void Start() => SceneManager.sceneLoaded += SceneManagerOnsceneLoaded;
        private void SceneManagerOnsceneLoaded(Scene scene, LoadSceneMode loadSceneMode) => _canvas.worldCamera = Camera.main;
        private void OnDisable() => SceneManager.sceneLoaded -= SceneManagerOnsceneLoaded;
    }
}