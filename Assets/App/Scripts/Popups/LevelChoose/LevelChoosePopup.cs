using Common.Configurations.Packs;
using Common.Data.Models;
using Common.Data.Repositories.Base;
using Libs.Popups;
using Popups.MainGame;
using Popups.PackChoose;
using SPopups.LevelChoose.Views;
using UnityEngine;
using UnityEngine.UI;
using IServiceProvider = Libs.Services.IServiceProvider;

namespace Popups.LevelChoose
{
    public class LevelChoosePopup : Popup
    {
        [SerializeField] private LevelsCollectionView _levelsCollectionView;
        [SerializeField] private Button _backButton;

        private IPackRepository _packRepository;

        private PackConfiguration _packConfiguration;
        private PackLevelCollection _packLevelCollection;
        private DefaultPackConfiguration _defaultPackConfiguration;

        protected override void InitializeProtected(IServiceProvider serviceProvider)
        {
            _packRepository = serviceProvider.GetRequiredService<IPackRepository>();
            _levelsCollectionView.LevelClicked += LevelsCollectionViewOnLevelClicked;
            ConfigureBackButton();
        }

        protected override void OnShowed()
        {
            TrySetPack();
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
            OnCloseSpawn<MainGamePopup>(withSetup: p =>
            {
                p.SetGameData(new GameData(_packConfiguration, _packLevelCollection, levelPreviewData));
            });
            
            PopupManager.CloseLastPopup();
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