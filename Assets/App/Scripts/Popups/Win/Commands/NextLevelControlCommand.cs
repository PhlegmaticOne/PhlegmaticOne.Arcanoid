using Common.Energy;
using Common.Packs.Data.Repositories.Base;
using Common.Game.Providers.Providers;
using Game;
using Game.Base;
using Libs.Popups.Base;
using Libs.Popups.ViewModels;
using Libs.Popups.ViewModels.Commands;

namespace Popups.Win.Commands
{
    public class NextLevelControlCommand : ParameterCommandBase<IPopupViewModel>
    {
        private readonly IGame<MainGameData, MainGameEvents> _game;
        private readonly EnergyManager _energyManager;
        private readonly IGameDataProvider _gameDataProvider;
        private readonly IPopupManager _popupManager;
        private readonly ILevelRepository _levelRepository;

        public NextLevelControlCommand(IGame<MainGameData, MainGameEvents> game,
            EnergyManager energyManager,
            IGameDataProvider gameDataProvider,
            IPopupManager popupManager, 
            ILevelRepository levelRepository)
        {
            _game = game;
            _energyManager = energyManager;
            _gameDataProvider = gameDataProvider;
            _popupManager = popupManager;
            _levelRepository = levelRepository;
        }

        protected override void Execute(IPopupViewModel parameter)
        {
            var gameData = _gameDataProvider.GetGameData();
            _energyManager.SpendEnergy(gameData.PackGameData.PackConfiguration.StartLevelEnergy);
            _game.Stop();
            
            parameter.CloseAction.AfterActionCommand = new StartGameCommand(_game, _gameDataProvider, _levelRepository);
            _popupManager.CloseLastPopup(false);
        }

        private class StartGameCommand : EmptyCommandBase
        {
            private readonly IGame<MainGameData, MainGameEvents> _game;
            private readonly IGameDataProvider _gameDataProvider;
            private readonly ILevelRepository _levelRepository;

            public StartGameCommand(IGame<MainGameData, MainGameEvents> game,
                IGameDataProvider gameDataProvider,
                ILevelRepository levelRepository)
            {
                _game = game;
                _gameDataProvider = gameDataProvider;
                _levelRepository = levelRepository;
            }
        
            protected override void Execute()
            {
                var gameData = _gameDataProvider.GetGameData();
                var levelData = _levelRepository.GetLevelData(gameData.PackGameData.PackPersistentData);
                _gameDataProvider.SetNewLevel(levelData);
                _game.StartGame(new MainGameData(levelData));
            }
        }
    }
}