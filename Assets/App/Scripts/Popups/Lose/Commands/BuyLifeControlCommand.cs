using Common.Bag;
using Common.Energy;
using Common.Packs.Data.Models;
using Game;
using Game.Base;
using Game.Logic.Systems.Health;
using Libs.Popups.Base;
using Libs.Popups.ViewModels;
using Libs.Popups.ViewModels.Commands;
using Popups.Common;

namespace Popups.Lose.Commands
{
    public class BuyLifeControlCommand : ParameterCommandBase<IPopupViewModel>
    {
        private readonly EnergyManager _energyManager;
        private readonly HealthSystem _healthSystem;
        private readonly IObjectBag _objectBag;
        private readonly IGame<MainGameData, MainGameEvents> _game;
        private readonly IPopupManager _popupManager;

        public BuyLifeControlCommand(EnergyManager energyManager, 
            HealthSystem healthSystem,
            IObjectBag objectBag,
            IGame<MainGameData, MainGameEvents> game,
            IPopupManager popupManager)
        {
            _energyManager = energyManager;
            _healthSystem = healthSystem;
            _objectBag = objectBag;
            _game = game;
            _popupManager = popupManager;
        }
        
        protected override bool CanExecute(IPopupViewModel parameter)
        {
            var packConfiguration = _objectBag.Get<GameData>().PackGameData.PackConfiguration;
            return _energyManager.CanSpendEnergy(packConfiguration.WinLevelEnergy);
        }

        protected override void Execute(IPopupViewModel parameter)
        {
            var packConfiguration = _objectBag.Get<GameData>().PackGameData.PackConfiguration;
            _energyManager.SpendEnergy(packConfiguration.ContinueLevelEnergy);
            _healthSystem.AddHealth();
            parameter.CloseAction.AfterActionCommand = new ContinueGameCommand(_game);
            _popupManager.CloseLastPopup();
        }
    }
}