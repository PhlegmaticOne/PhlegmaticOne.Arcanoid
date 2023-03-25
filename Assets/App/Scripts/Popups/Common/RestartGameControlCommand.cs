using Common.Bag;
using Common.Energy;
using Common.Packs.Data.Models;
using Game;
using Game.Base;
using Libs.Popups.Base;
using Libs.Popups.ViewModels;
using Libs.Popups.ViewModels.Commands;
using Popups.Common;

namespace Popups.Common.Commands
{
    public class RestartControlCommand : ParameterCommandBase<IPopupViewModel>
    {
        private readonly EnergyManager _energyManager;
        private readonly IObjectBag _objectBag;
        private readonly IGame<MainGameData, MainGameEvents> _game;
        private readonly IPopupManager _popupManager;

        public RestartControlCommand(EnergyManager energyManager, IObjectBag objectBag,
            IGame<MainGameData, MainGameEvents> game, IPopupManager popupManager)
        {
            _energyManager = energyManager;
            _objectBag = objectBag;
            _game = game;
            _popupManager = popupManager;
        }
        
        protected override bool CanExecute(IPopupViewModel parameter)
        {
            var packConfiguration = _objectBag.Get<GameData>().PackGameData.PackConfiguration;
            return _energyManager.CanSpendEnergy(packConfiguration.StartLevelEnergy);
        }

        protected override void Execute(IPopupViewModel parameter)
        {
            var packConfiguration = _objectBag.Get<GameData>().PackGameData.PackConfiguration;
            _energyManager.SpendEnergy(packConfiguration.StartLevelEnergy);
            parameter.CloseAction.AfterActionCommand = new RestartMainGameCommand(_game, _objectBag);
            _popupManager.CloseLastPopup(false);
        }
    }
    
    public class RestartMainGameCommand : EmptyCommandBase
    {
        private readonly IGame<MainGameData, MainGameEvents> _mainGame;
        private readonly IObjectBag _objectBag;

        public RestartMainGameCommand(IGame<MainGameData, MainGameEvents> mainGame, IObjectBag objectBag)
        {
            _mainGame = mainGame;
            _objectBag = objectBag;
        }
        
        protected override void Execute()
        {
            _mainGame.Stop();
            var currentLevelData = _objectBag.Get<LevelData>();
            _mainGame.StartGame(new MainGameData(currentLevelData));
        }
    }
}