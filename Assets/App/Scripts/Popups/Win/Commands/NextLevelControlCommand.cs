using Common.Bag;
using Common.Energy;
using Common.Packs.Data.Models;
using Common.Packs.Data.Repositories.Base;
using Game;
using Game.Base;
using Libs.Popups.Base;
using Libs.Popups.ViewModels;
using Libs.Popups.ViewModels.Commands;
using Popups.MainGame.Commands;
using UnityEngine;

namespace Popups.Win.Commands
{
    public class NextLevelControlCommand : ParameterCommandBase<IPopupViewModel>
    {
        private readonly IGame<MainGameData, MainGameEvents> _game;
        private readonly EnergyManager _energyManager;
        private readonly IObjectBag _objectBag;
        private readonly IPopupManager _popupManager;
        private readonly ILevelRepository _levelRepository;

        public NextLevelControlCommand(IGame<MainGameData, MainGameEvents> game,
            EnergyManager energyManager,
            IObjectBag objectBag,
            IPopupManager popupManager, 
            ILevelRepository levelRepository)
        {
            _game = game;
            _energyManager = energyManager;
            _objectBag = objectBag;
            _popupManager = popupManager;
            _levelRepository = levelRepository;
        }

        protected override void Execute(IPopupViewModel parameter)
        {
            var gameData = _objectBag.Get<GameData>();
            _energyManager.SpendEnergy(gameData.PackGameData.PackConfiguration.StartLevelEnergy);
            _game.Stop();
            
            parameter.CloseAction.AfterActionCommand =
                new MainGamePopupOnShowCommand(_game, _objectBag, _levelRepository);
            _popupManager.CloseLastPopup(false);
        }
    }
}