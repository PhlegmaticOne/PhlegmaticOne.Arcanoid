using System.Collections.Generic;
using System.Linq;
using Abstracts.Popups;
using Abstracts.Popups.Base;
using Scenes.ChoosePackPopup.Views;
using Scenes.MainGameScene.Configurations.Packs;
using Scenes.MainGameScene.Data.Repositories.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Scenes.Popups
{
    public class ChoosePackPopup : Popup
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
            var packConfigurations = _packRepository.GetAll().ToList();

            if (_packRepository.PacksInitialized == false)
            {
                InitializePackConfigurations(packConfigurations);
            }
            
            _packCollectionView.PackClicked += PackCollectionViewOnPackClicked;
            _packCollectionView.ShowPacks(packConfigurations);
        }

        private void PackCollectionViewOnPackClicked(PackConfiguration packConfiguration)
        {
            _onHidSpawnAction = () =>
            {
                var popup = _popupManager.SpawnPopup<ChooseLevelPopup>();
                popup.SetPack(packConfiguration);
            };
            _popupManager.HidePopup();
        }

        protected override void OnHid()
        {
            _onHidSpawnAction?.Invoke();
        }

        private void ConfigureBackButton()
        {
            _backButton.onClick.AddListener(() =>
            {
                _onHidSpawnAction = () => _popupManager.SpawnPopup<StartPopup>();
                _popupManager.HidePopup();
            });
        }

        private void InitializePackConfigurations(List<PackConfiguration> packConfigurations)
        {
            foreach (var packConfiguration in packConfigurations)
            {
                var levelsCount = _packRepository.GetLevelsCount(packConfiguration.Name);
                packConfiguration.SetLevelsCount(levelsCount);
                _packRepository.Save(packConfiguration);
            }
            _packRepository.MarkAsInitialized();
            _packRepository.Save();
        }
    }
}