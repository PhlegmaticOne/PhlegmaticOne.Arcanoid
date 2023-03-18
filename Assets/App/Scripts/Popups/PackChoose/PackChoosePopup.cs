using System.Collections.Generic;
using System.Linq;
using Common.Bag;
using Common.Energy;
using Common.Packs.Data.Models;
using Common.Packs.Data.Repositories.Base;
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
        [SerializeField] private EnergyView _energyView;

        private IPackRepository _packRepository;
        private IObjectBag _objectBag;
        private ILocalizationManager _localizationManager;
        private LocalizationContext _localizationContext;
        private EnergyController _energyController;
        
        public IEnumerable<ILocalizationBindable> GetBindableComponents()
        {
            return _bindableComponents.Concat(_packCollectionView.GetBindableComponents());
        }

        protected override void InitializeProtected(IServiceProvider serviceProvider)
        {
            _packRepository = serviceProvider.GetRequiredService<IPackRepository>();
            _localizationManager = serviceProvider.GetRequiredService<ILocalizationManager>();
            _objectBag = serviceProvider.GetRequiredService<IObjectBag>();

            var energyManager = serviceProvider.GetRequiredService<EnergyManager>();
            _energyController = new EnergyController(energyManager, _energyView);
            
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
            _energyController.Disable();
            _energyController = null;
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

        private void PackCollectionViewOnPackClicked(PackGameData packGameData)
        {
            var configuration = packGameData.PackConfiguration;
            _energyController.SpendEnergy(configuration.StartLevelEnergy, () =>
            {
                SetGameData(packGameData);
                PopupManager.CloseAllPopupsInstant();
                SceneManager.LoadScene(1);
            });
        }

        private void SetGameData(PackGameData packGameData)
        {
            var packPersistentData = packGameData.PackPersistentData;
            var packLevelCollection = _packRepository.GetLevelsForPack(packPersistentData);
            var currentLevelIdIndex = packPersistentData.passedLevelsCount;
            
            if (packPersistentData.IsPassed)
            {
                packPersistentData.passedLevelsCount = 0;
                _packRepository.Save(packPersistentData);
                currentLevelIdIndex = 0;
            }
            
            var currentLevel = packLevelCollection.levelIds[currentLevelIdIndex];
            packPersistentData.currentLevelId = currentLevel;
            var gameData = new GameData(packGameData, packLevelCollection);
            _objectBag.Set(gameData);
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