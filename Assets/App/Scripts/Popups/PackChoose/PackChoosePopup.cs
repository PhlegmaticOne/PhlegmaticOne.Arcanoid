using Common.Configurations.Packs;
using Common.Data.Repositories.Base;
using Libs.Popups;
using Libs.Services;
using Popups.LevelChoose;
using Popups.PackChoose.Views;
using Popups.Start;
using UnityEngine;
using UnityEngine.UI;

namespace Popups.PackChoose
{
    public class PackChoosePopup : Popup
    {
        [SerializeField] private PackCollectionView _packCollectionView;
        [SerializeField] private Button _backButton;

        private IPackRepository _packRepository;

        protected override void InitializeProtected(IServiceProvider serviceProvider)
        {
            _packRepository = serviceProvider.GetRequiredService<IPackRepository>();
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
            RemoveAllListeners(_backButton);
            _packCollectionView.PackClicked -= PackCollectionViewOnPackClicked;
            _packCollectionView.Clear();
        }

        private void ShowPacks()
        {
            _packCollectionView.PackClicked += PackCollectionViewOnPackClicked;
            var packConfigurations = _packRepository.GetAll();
            _packCollectionView.ShowPacks(packConfigurations);
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