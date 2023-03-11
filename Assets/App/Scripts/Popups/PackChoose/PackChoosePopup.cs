using System.Collections.Generic;
using System.Linq;
using Common.Configurations.Packs;
using Common.Data.Models;
using Common.Data.Repositories.Base;
using Game.Accessors;
using Libs.Localization.Base;
using Libs.Localization.Components.Base;
using Libs.Localization.Context;
using Libs.Popups;
using Libs.Services;
using Popups.PackChoose.Views;
using Popups.Start;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Popups.PackChoose
{
    public class PackChoosePopup : Popup, ILocalizable
    {
        [SerializeField] private PackCollectionView _packCollectionView;
        [SerializeField] private Button _backButton;
        [SerializeField] private List<LocalizationBindableComponent> _bindableComponents;

        private IPackRepository _packRepository;
        private IObjectAccessor<GameData> _gameDataAccessor;
        private ILocalizationManager _localizationManager;
        private LocalizationContext _localizationContext;
        
        public IEnumerable<ILocalizationBindable> GetBindableComponents()
        {
            return _bindableComponents.Concat(_packCollectionView.GetBindableComponents());
        }

        protected override void InitializeProtected(IServiceProvider serviceProvider)
        {
            _packRepository = serviceProvider.GetRequiredService<IPackRepository>();
            _localizationManager = serviceProvider.GetRequiredService<ILocalizationManager>();
            _gameDataAccessor = serviceProvider.GetRequiredService<IObjectAccessor<GameData>>();
            ConfigureBackButton();
            ShowPacks();
        }

        public override void EnableInput()
        {
            EnableBehaviour(_backButton);
        }
        
        public override void DisableInput()
        {
            DisableBehaviour(_backButton);
        }

        public override void Reset()
        {
            _localizationContext.Flush();
            _localizationContext = null;
            RemoveAllListeners(_backButton);
            _packCollectionView.PackClicked -= PackCollectionViewOnPackClicked;
            _packCollectionView.Clear();
        }

        private void ShowPacks()
        {
            _packCollectionView.PackClicked += PackCollectionViewOnPackClicked;
            var packConfigurations = _packRepository.GetAll();
            _packCollectionView.ShowPacks(packConfigurations);
            
            _localizationContext = LocalizationContext
                .Create(_localizationManager)
                .BindLocalizable(this)
                .Refresh();
        }

        private void PackCollectionViewOnPackClicked(PackConfiguration packConfiguration)
        {
            SetGameData(packConfiguration);
            PopupManager.CloseAllPopupsInstant();
            SceneManager.LoadScene(1);
        }

        private void SetGameData(PackConfiguration packConfiguration)
        {
            var packLevelCollection = _packRepository.GetLevels(packConfiguration.Name);
            var currentLevelIdIndex = packConfiguration.PassedLevelsCount;

            if (packConfiguration.IsPassed)
            {
                packConfiguration.ResetPassedLevelsCount();
                _packRepository.Save(packLevelCollection);
                _packRepository.Save();
                currentLevelIdIndex = 0;
            }
            
            var currentLevel = packLevelCollection.LevelPreviews[currentLevelIdIndex];
            var gameData = new GameData(packConfiguration, packLevelCollection, currentLevel);
            _gameDataAccessor.Set(gameData);
        }

        private void ConfigureBackButton()
        {
            _backButton.onClick.AddListener(() =>
            {
                OnCloseSpawn<StartPopup>();
                PopupManager.CloseLastPopup();
            });
        }
    }
}