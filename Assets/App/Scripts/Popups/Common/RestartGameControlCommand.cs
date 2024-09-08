using Common.Energy;
using Common.Game.Providers.Providers;
using Game;
using Game.Base;
using Libs.Popups.Base;
using Libs.Popups.ViewModels;
using Libs.Popups.ViewModels.Commands;

namespace Popups.Common
{
    public class RestartControlCommand : ParameterCommandBase<IPopupViewModel>
    {
        private readonly EnergyManager _energyManager;
        private readonly IGameDataProvider _gameDataProvider;
        private readonly IGame<MainGameData, MainGameEvents> _game;
        private readonly IPopupManager _popupManager;

        public RestartControlCommand(EnergyManager energyManager, IGameDataProvider gameDataProvider,
            IGame<MainGameData, MainGameEvents> game, IPopupManager popupManager)
        {
            _energyManager = energyManager;
            _gameDataProvider = gameDataProvider;
            _game = game;
            _popupManager = popupManager;
        }
        
        protected override bool CanExecute(IPopupViewModel parameter)
        {
            var packConfiguration = _gameDataProvider.GetGameData().PackGameData.PackConfiguration;
            return _energyManager.CanSpendEnergy(packConfiguration.StartLevelEnergy);
        }

        protected override void Execute(IPopupViewModel parameter)
        {
            var packConfiguration = _gameDataProvider.GetGameData().PackGameData.PackConfiguration;
            _energyManager.SpendEnergy(packConfiguration.StartLevelEnergy);
            parameter.CloseAction.AfterActionCommand = new RestartMainGameCommand(_game, _gameDataProvider);
            _popupManager.CloseLastPopup(false);
        }
    }
    
    public class RestartMainGameCommand : EmptyCommandBase
    {
        private readonly IGame<MainGameData, MainGameEvents> _mainGame;
        private readonly IGameDataProvider _gameDataProvider;

        public RestartMainGameCommand(IGame<MainGameData, MainGameEvents> mainGame, IGameDataProvider gameDataProvider)
        {
            _mainGame = mainGame;
            _gameDataProvider = gameDataProvider;
        }
        
        protected override void Execute()
        {
            _mainGame.Stop();
            var currentLevelData = _gameDataProvider.GetGameData().CurrentLevel;
            _mainGame.StartGame(new MainGameData(currentLevelData));
        }
    }
}