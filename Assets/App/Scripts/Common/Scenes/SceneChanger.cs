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

        private IList<Popup> _spawnedPopups;
        private SceneTransitionPopup _transitionPopup;
        private int _sceneIndex;

        public SceneChanger(IPopupManager popupManager)
        {
            _popupManager = popupManager;
        }
        
        public void ChangeScene(int sceneIndex)
        {
            _sceneIndex = sceneIndex;
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
            
            SceneManager.sceneLoaded += SceneManagerOnsceneLoaded;
            SceneManager.LoadScene(_sceneIndex);
        }

        private void SceneManagerOnsceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            _transitionPopup.Closed += TransitionPopupOnClosed;
            _popupManager.ClosePopup(_transitionPopup, false);
            SceneManager.sceneLoaded -= SceneManagerOnsceneLoaded;
        }

        private void TransitionPopupOnClosed(Popup popup)
        {
            _transitionPopup.Closed -= TransitionPopupOnClosed;
            SceneChanged?.Invoke();
        }

        public event Action SceneChanged;
        public event Action OnOverlay;
    }
}