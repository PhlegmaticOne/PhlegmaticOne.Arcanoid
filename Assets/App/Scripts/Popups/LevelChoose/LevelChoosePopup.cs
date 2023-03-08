using Common.Configurations.Packs;
using Common.Data.Models;
using Common.Data.Providers;
using Common.Data.Repositories.Base;
using Libs.Popups;
using Popups.PackChoose;
using SPopups.LevelChoose.Views;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using IServiceProvider = Libs.Services.IServiceProvider;

namespace Popups.LevelChoose
{
    public class LevelChoosePopup : Popup
    {
        [SerializeField] private LevelsCollectionView _levelsCollectionView;
        [SerializeField] private Button _backButton;

        private IPackRepository _packRepository;
        private GameDataProvider _gameDataProvider;
        
        private PackConfiguration _packConfiguration;
        private PackLevelCollection _packLevelCollection;
        private DefaultPackConfiguration _defaultPackConfiguration;

        protected override void InitializeProtected(IServiceProvider serviceProvider)
        {
            _packRepository = serviceProvider.GetRequiredService<IPackRepository>();
            _gameDataProvider = serviceProvider.GetRequiredService<GameDataProvider>();
            _levelsCollectionView.LevelClicked += LevelsCollectionViewOnLevelClicked;
            ConfigureBackButton();
        }

        protected override void OnShowed() => TrySetPack();

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
        
        public override void Reset()
        {
            RemoveAllListeners(_backButton);
            _levelsCollectionView.LevelClicked -= LevelsCollectionViewOnLevelClicked;
            _levelsCollectionView.Clear();
        }
        
        public void SetPack(PackConfiguration packConfiguration)
        {
            var levels = _packRepository.GetLevels(packConfiguration.Name);
            _packConfiguration = packConfiguration;
            _packLevelCollection = levels;
            _levelsCollectionView.ShowLevels(levels, packConfiguration);
        }

        private void LevelsCollectionViewOnLevelClicked(LevelPreviewData levelPreviewData)
        {
            var gameData = new GameData(_packConfiguration, _packLevelCollection, levelPreviewData);
            _gameDataProvider.Set(gameData);
            PopupManager.CloseAllPopupsInstant();
            SceneManager.LoadScene(1);
        }

        private void TrySetPack()
        {
            if (_packConfiguration != null)
            {
                return;
            }
            
            SetPack(_packRepository.DefaultPackConfiguration.DefaultPack);
        }

        private void ConfigureBackButton()
        {
            _backButton.onClick.AddListener(() =>
            {
                OnCloseSpawn<PackChoosePopup>();
                PopupManager.CloseLastPopup();
            });
        }
    }
}