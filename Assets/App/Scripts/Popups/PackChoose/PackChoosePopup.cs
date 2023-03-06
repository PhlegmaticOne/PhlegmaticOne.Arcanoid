using Common.Configurations.Packs;
using Common.Data.Repositories.Base;
using Libs.Popups;
using Libs.Popups.Base;
using Popups.LevelChoose;
using Popups.PackChoose.Views;
using Popups.Start;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Popups.PackChoose
{
    public class PackChoosePopup : Popup
    {
        [SerializeField] private PackCollectionView _packCollectionView;
        [SerializeField] private Button _backButton;

        private IPopupManager _popupManager;
        private IPackRepository _packRepository;
        private UnityAction _onHidSpawnAction;
        public void Initialize(IPopupManager popupManager, IPackRepository packRepository)
        {
            _popupManager = popupManager;
            _packRepository = packRepository;
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
            _onHidSpawnAction = () =>
            {
                var popup = _popupManager.SpawnPopup<LevelChoosePopup>();
                popup.SetPack(packConfiguration);
            };
            _popupManager.CloseLastPopup();
        }

        protected override void OnClosed()
        {
            _onHidSpawnAction?.Invoke();
        }

        private void ConfigureBackButton()
        {
            _backButton.onClick.AddListener(() =>
            {
                _onHidSpawnAction = () => _popupManager.SpawnPopup<StartPopup>();
                _popupManager.CloseLastPopup();
            });
        }
    }
}