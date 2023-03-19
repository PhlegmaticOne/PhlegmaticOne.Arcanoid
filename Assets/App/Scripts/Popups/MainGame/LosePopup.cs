using System.Collections.Generic;
using Common.Bag;
using Common.Energy;
using Common.Packs.Configurations;
using Common.Packs.Data.Models;
using Game.PopupRequires.Commands.Base;
using Game.PopupRequires.ViewModels;
using Libs.Localization.Base;
using Libs.Localization.Components.Base;
using Libs.Localization.Context;
using Libs.Popups;
using Libs.Services;
using Popups.Energy;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Popups.MainGame
{
    public class LosePopup : Popup, ILocalizable
    {
        [SerializeField] private string _restartReasonPhraseKey;
        [SerializeField] private string _continueReasonPhraseKey;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _buyLifeButton;
        [SerializeField] private Button _backButton;
        [SerializeField] private TextMeshProUGUI _restartEnergyText;
        [SerializeField] private TextMeshProUGUI _continueEnergyText;
        [SerializeField] private EnergyView _energyView;
        [SerializeField] private List<LocalizationBindableComponent> _bindableComponents;

        private ILocalizationManager _localizationManager;
        private LocalizationContext _localizationContext;

        private LosePopupViewModel _losePopupViewModel;
        private IObjectBag _objectBag;
        private UnityAction _onCloseAction;
        private ICommand _onCloseCommand;
        private EnergyController _energyController;
        
        public IEnumerable<ILocalizationBindable> GetBindableComponents() => _bindableComponents;

        protected override void InitializeProtected(IServiceProvider serviceProvider)
        {
            _localizationManager = serviceProvider.GetRequiredService<ILocalizationManager>();
            var energyManager = serviceProvider.GetRequiredService<EnergyManager>();
            _objectBag = serviceProvider.GetRequiredService<IObjectBag>();
            _energyController = new EnergyController(energyManager, _energyView);
            
            _localizationContext = LocalizationContext
                .Create(_localizationManager)
                .BindLocalizable(this)
                .Refresh();
            
            ConfigureRestartButton();
            ConfigureBuyLifeButton();
            ConfigureBackButton();
            ShowEnergyInfo();
        }

        public void SetupViewModel(LosePopupViewModel losePopupViewModel) => _losePopupViewModel = losePopupViewModel;
        public void OnShowing() => _losePopupViewModel.OnShowingCommand.Execute();
        public void OnRestart(UnityAction action) => _onCloseAction = action;

        public override void EnableInput()
        {
            EnableBehaviour(_restartButton);
            EnableBehaviour(_buyLifeButton);
            EnableBehaviour(_backButton);
        }

        public override void DisableInput()
        {
            DisableBehaviour(_restartButton);
            DisableBehaviour(_buyLifeButton);
            DisableBehaviour(_backButton);
        }

        protected override void OnClosed()
        {
            _onCloseCommand?.Execute();
            _onCloseAction?.Invoke();
        }

        public override void Reset()
        {
            _localizationContext.Flush();
            _localizationContext = null;
            _energyController.Disable();
            _energyController = null;
            _onCloseAction = null;
            RemoveAllListeners(_restartButton);
            RemoveAllListeners(_buyLifeButton);
            RemoveAllListeners(_backButton);
        }

        private void ShowEnergyInfo()
        {
            var packConfiguration = GetPackConfiguration();
            _restartEnergyText.text = packConfiguration.StartLevelEnergy.ToString();
            _continueEnergyText.text = packConfiguration.ContinueLevelEnergy.ToString();
        }

        private void ConfigureRestartButton()
        {
            _restartButton.onClick.AddListener(() =>
            {
                var packConfiguration = GetPackConfiguration();
                
                if (_energyController.CanSpendEnergy(packConfiguration.StartLevelEnergy) == false)
                {
                    var popup = PopupManager.SpawnPopup<EnergyPopup>();
                    popup.ShowWithReasonPhraseKey(_restartReasonPhraseKey);
                    return;
                }
                
                _onCloseCommand = _losePopupViewModel.RestartButtonCommand;
                _energyController.SpendEnergy(packConfiguration.StartLevelEnergy);
                PopupManager.CloseLastPopup();
            });
        }

        private void ConfigureBuyLifeButton()
        {
            _buyLifeButton.onClick.AddListener(() =>
            {
                var packConfiguration = GetPackConfiguration();
                
                if (_energyController.CanSpendEnergy(packConfiguration.ContinueLevelEnergy) == false)
                {
                    var popup = PopupManager.SpawnPopup<EnergyPopup>();
                    popup.ShowWithReasonPhraseKey(_continueReasonPhraseKey);
                    return;
                }
                
                _energyController.SpendEnergy(packConfiguration.ContinueLevelEnergy);
                _onCloseCommand = _losePopupViewModel.BuyLifeButtonCommand;
                _onCloseAction = null;
                PopupManager.CloseLastPopup();
            });
        }

        private void ConfigureBackButton()
        {
            _backButton.onClick.AddListener(() =>
            {
                _onCloseAction = null;
                _onCloseCommand = _losePopupViewModel.BackButtonCommand;
                PopupManager.CloseLastPopup();
            });
        }

        private PackConfiguration GetPackConfiguration()
        {
            return _objectBag.Get<GameData>().PackGameData.PackConfiguration;
        }
    }
}