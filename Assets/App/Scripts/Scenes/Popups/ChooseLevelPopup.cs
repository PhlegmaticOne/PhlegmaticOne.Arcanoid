using System;
using Abstracts.Popups;
using Abstracts.Popups.Base;
using Scenes.LevelChoosePopup.Views;
using Scenes.MainGameScene.Configurations.Packs;
using Scenes.MainGameScene.Data;
using Scenes.MainGameScene.Data.Repositories.Base;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.Popups
{
    public class ChooseLevelPopup : Popup
    {
        [SerializeField] private LevelsCollectionView _levelsCollectionView;
        [SerializeField] private Button _backButton;

        private IPopupManager _popupManager;
        private IPackRepository _packRepository;
        private Action _onHidAction;

        public void Initialize(IPopupManager popupManager, IPackRepository packRepository)
        {
            _popupManager = popupManager;
            _packRepository = packRepository;
            _levelsCollectionView.LevelClicked += LevelsCollectionViewOnLevelClicked;
            ConfigureBackButton();
        }

        private void LevelsCollectionViewOnLevelClicked(LevelPreviewData levelPreviewData)
        {
            _onHidAction = () => _popupManager.SpawnPopup<MainGamePopup>();
            _popupManager.HidePopup();
        }

        public void SetPack(PackConfiguration packConfiguration)
        {
            var levels = _packRepository.GetLevels(packConfiguration.Name);
            _levelsCollectionView.ShowLevels(levels, packConfiguration);
        }

        public override void EnableInput()
        {
            _levelsCollectionView.EnableLevels();   
            EnableBehaviour(_backButton);
        }

        public override void DisableInput()
        {
            _levelsCollectionView.DisableLevels();
            DisableBehaviour(_backButton);
        }

        protected override void OnHid() => _onHidAction?.Invoke();

        public override void Reset()
        {
            RemoveAllListeners(_backButton);
            _levelsCollectionView.LevelClicked -= LevelsCollectionViewOnLevelClicked;
            _levelsCollectionView.Clear();
        }

        private void ConfigureBackButton()
        {
            _backButton.onClick.AddListener(() =>
            {
                _onHidAction = () => _popupManager.SpawnPopup<ChoosePackPopup>();
                _popupManager.HidePopup();
            });
        }
    }
}