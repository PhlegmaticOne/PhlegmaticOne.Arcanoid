using Common.Bag;
using Common.Energy;
using Common.Packs.Configurations;
using Common.Packs.Data.Models;
using Common.Packs.Data.Repositories.Base;
using Common.Scenes;
using Game;
using Game.Base;
using Libs.Popups.Base;
using Libs.Popups.ViewModels.Actions;
using Popups.Common;
using Popups.Common.Commands;
using Popups.Win.Commands;
using UnityEngine;

namespace Popups.Win.Factory
{
    public class WinPopupViewModelFactory : IWinPopupViewModelFactory
    {
        private readonly IObjectBag _objectBag;
        private readonly IGame<MainGameData, MainGameEvents> _game;
        private readonly IPopupManager _popupManager;
        private readonly ILevelRepository _levelRepository;
        private readonly IPackRepository _packRepository;
        private readonly EnergyManager _energyManager;
        private readonly ISceneChanger _sceneChanger;

        public WinPopupViewModelFactory(IObjectBag objectBag, 
            IGame<MainGameData, MainGameEvents> game,
            IPopupManager popupManager,
            ILevelRepository levelRepository,
            IPackRepository packRepository,
            EnergyManager energyManager,
            ISceneChanger sceneChanger)
        {
            _objectBag = objectBag;
            _game = game;
            _popupManager = popupManager;
            _levelRepository = levelRepository;
            _packRepository = packRepository;
            _energyManager = energyManager;
            _sceneChanger = sceneChanger;
        }
        
        public WinPopupViewModel CreateWinPopupViewModel()
        {
            var packGameData = _objectBag.Get<GameData>().PackGameData;
            var persistentData = packGameData.PackPersistentData;
            
            var pauseGameCommand = new PauseGameCommand(_game);
            var addEnergyCommand = new WinMenuOnShowCommand(_energyManager, _packRepository, _objectBag);
            var backAction = new BackControlCommand(_game, _popupManager, _sceneChanger);

            var result = new WinPopupViewModel
            {
                CloseAction = PopupAction.Empty,
                ShowAction = new PopupAction(pauseGameCommand, addEnergyCommand),
                BackControlAction = new ControlAction(backAction),
                CurrentPackData = packGameData
            };

            if (persistentData.IsCurrentlyPassed() == false)
            {
                return InitForNextLevel(result);
            }

            var nextPackGameData = GetNextPackGameData(packGameData.PackConfiguration);

            if (nextPackGameData == null)
            {
                return InitForAllPacksPassed(result);
            }

            return persistentData.IsPassedFirstTime() ? 
                InitForNextPack(result, nextPackGameData) : 
                InitForPackPassedMultipleTimes(result);
        }

        private WinPopupViewModel InitForNextLevel(WinPopupViewModel result)
        {
            result.WinState = WinState.NextLevelInCurrentPack;
            var nextLevelControlAction = new NextLevelControlCommand(_game, _energyManager,
                _objectBag, _popupManager, _levelRepository);
            result.NextControlAction = new ControlAction(nextLevelControlAction);
            return result;
        }
        
        private WinPopupViewModel InitForAllPacksPassed(WinPopupViewModel result)
        {
            var backAction = new BackControlCommand(_game, _popupManager, _sceneChanger);
            result.WinState = WinState.AllPacksPassed;
            result.NextControlAction = new ControlAction(backAction);
            return result;
        }
        
        private WinPopupViewModel InitForNextPack(WinPopupViewModel result, PackGameData nextPackGameData)
        {
            var nextCommand = new TransitToNextPackFirstLevelCommand(_game, _energyManager,
                _objectBag, _popupManager, _packRepository, _levelRepository);
            result.NextPackData = nextPackGameData;
            nextCommand.SetNextPackGameData(nextPackGameData);
            result.WinState = WinState.PackPassedFirstTime;
            result.NextControlAction = new ControlAction(nextCommand);
            return result;
        }
        
        private WinPopupViewModel InitForPackPassedMultipleTimes(WinPopupViewModel result)
        {
            var backAction = new BackControlCommand(_game, _popupManager, _sceneChanger);
            result.WinState = WinState.PackPassedMultipleTime;
            result.NextControlAction = new ControlAction(backAction);
            return result;
        }

        private PackGameData GetNextPackGameData(PackConfiguration current)
        {
            var nextConfiguration = _packRepository.GetNextPackConfiguration(current);
            
            if (nextConfiguration == null)
            {
                return null;
            }

            var nextPersistentData = _packRepository.GetPersistentDataForPack(nextConfiguration);
            return new PackGameData(nextConfiguration, nextPersistentData);
        }
    }
}