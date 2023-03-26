using System;
using System.Collections.Generic;
using Libs.Popups;
using Libs.Popups.Base;
using Popups.Transition;
using UnityEngine.SceneManagement;

namespace Common.Scenes
{
    public class SceneChanger : ISceneChanger
    {
        private readonly IPopupManager _popupManager;
        private readonly IScenesProvider _scenesProvider;

        private IList<Popup> _spawnedPopups;
        private SceneTransitionPopup _transitionPopup;
        private SceneInfo _tempScene;

        public SceneChanger(IPopupManager popupManager, IScenesProvider scenesProvider)
        {
            _popupManager = popupManager;
            _scenesProvider = scenesProvider;
            SceneManager.sceneLoaded += SceneManagerOnsceneLoaded;
        }
        
        public event Action SceneChanged;
        public event Action OnOverlay;
        
        public SceneInfo CurrentScene { get; private set; }

        public void ChangeScene(string sceneKey)
        {
            var scene = _scenesProvider.GetSceneByCustomKey(sceneKey);
            _tempScene = scene;
            _spawnedPopups = _popupManager.GetAll();
            _transitionPopup = _popupManager.SpawnPopup<SceneTransitionPopup>();
            _transitionPopup.Showed += TransitionPopupOnShowed;
        }

        private void TransitionPopupOnShowed(Popup popup)
        {
            _transitionPopup.Showed -= TransitionPopupOnShowed;
            
            foreach (var spawnedPopup in _spawnedPopups)
            {
                _popupManager.ClosePopupInstant(spawnedPopup);
            }
            
            OnOverlay?.Invoke();
            SceneManager.LoadScene(_tempScene.Scene.name);
        }

        private void SceneManagerOnsceneLoaded(Scene scene, LoadSceneMode arg1)
        {
            if (_tempScene == null)
            {
                CurrentScene = _scenesProvider.GetSceneBySceneName(scene.name);
            }
            else
            {
                CurrentScene = _scenesProvider.GetSceneByCustomKey(_tempScene.Key);
                _tempScene = null;
                _transitionPopup.Closed += TransitionPopupOnClosed;
                _popupManager.ClosePopup(_transitionPopup, false);
            }
        }

        private void TransitionPopupOnClosed(Popup popup)
        {
            _transitionPopup.Closed -= TransitionPopupOnClosed;
            SceneChanged?.Invoke();
        }
    }
}