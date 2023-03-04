using System;
using Common.Configurations.Packs;
using Common.Data.Models;
using Common.Data.Repositories.Base;
using Libs.Popups;
using Libs.Popups.Base;
using Popups.MainGame;
using Popups.PackChoose;
using SPopups.LevelChoose.Views;
using UnityEngine;
using UnityEngine.UI;

namespace Popups.LevelChoose
{
    public class LevelChoosePopup : Popup
    {
        [SerializeField] private LevelsCollectionView _levelsCollectionView;
        [SerializeField] private Button _backButton;

        private IPopupManager _popupManager;
        private IPackRepository _packRepository;
        private Action _onHidAction;

        private PackConfiguration _packConfiguration;
        private PackLevelCollection _packLevelCollection;

        public void Initialize(IPopupManager popupManager, IPackRepository packRepository)
        {
            _popupManager = popupManager;
            _packRepository = packRepository;
            _levelsCollectionView.LevelClicked += LevelsCollectionViewOnLevelClicked;
            ConfigureBackButton();
        }

        private void LevelsCollectionViewOnLevelClicked(LevelPreviewData levelPreviewData)
        {
            _onHidAction = () =>
            {
                var mainGamePopup = _popupManager.SpawnPopup<MainGamePopup>();
                mainGamePopup.SetGameData(new GameData(_packConfiguration, _packLevelCollection, levelPreviewData));
            };
            _popupManager.HidePopup();
        }

        public void SetPack(PackConfiguration packConfiguration)
        {
            var levels = _packRepository.GetLevels(packConfiguration.Name);
            _packConfiguration = packConfiguration;
            _packLevelCollection = levels;
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
                _onHidAction = () => _popupManager.SpawnPopup<PackChoosePopup>();
                _popupManager.HidePopup();
            });
        }
    }
}