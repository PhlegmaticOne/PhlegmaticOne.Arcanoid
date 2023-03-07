using System.Collections.Generic;
using System.Linq;
using Common.Configurations.Packs;
using Common.Data.Repositories.Base;
using Libs.Localization.Base;
using Libs.Localization.Components.Base;
using Libs.Localization.Context;
using Libs.Popups;
using Libs.Services;
using Popups.LevelChoose;
using Popups.PackChoose.Views;
using Popups.Start;
using UnityEngine;
using UnityEngine.UI;

namespace Popups.PackChoose
{
    public class PackChoosePopup : Popup, ILocalizable
    {
        [SerializeField] private PackCollectionView _packCollectionView;
        [SerializeField] private Button _backButton;
        [SerializeField] private List<LocalizationBindableComponent> _bindableComponents;

        private IPackRepository _packRepository;
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
            OnCloseSpawn<LevelChoosePopup>(withSetup: p => p.SetPack(packConfiguration));
            PopupManager.CloseLastPopup();
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