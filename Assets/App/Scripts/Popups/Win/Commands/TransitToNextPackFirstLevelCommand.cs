using Common.Energy;
using Common.Game.Providers;
using Common.Packs.Data.Models;
using Common.Packs.Data.Repositories.Base;
using Common.Game.Providers.Providers;
using Game;
using Game.Base;
using Libs.Popups.Base;
using Libs.Popups.ViewModels;
using Libs.Popups.ViewModels.Commands;

namespace Popups.Win.Commands
{
    public class TransitToNextPackFirstLevelCommand : ParameterCommandBase<IPopupViewModel>
    {
        private readonly IGame<MainGameData, MainGameEvents> _game;
        private readonly EnergyManager _energyManager;
        private readonly IGameDataProvider _gameDataProvider;
        private readonly IPopupManager _popupManager;
        private readonly IPackRepository _packRepository;
        private readonly ILevelRepository _levelRepository;

        private PackGameData _packGameData;

        public TransitToNextPackFirstLevelCommand(IGame<MainGameData, MainGameEvents> game,
            EnergyManager energyManager,
            IGameDataProvider gameDataProvider,
            IPopupManager popupManager, 
            IPackRepository packRepository,
            ILevelRepository levelRepository)
        {
            _game = game;
            _energyManager = energyManager;
            _gameDataProvider = gameDataProvider;
            _popupManager = popupManager;
            _packRepository = packRepository;
            _levelRepository = levelRepository;
        }

        public void SetNextPackGameData(PackGameData packGameData)
        {
            _packGameData = packGameData;
            _packGameData.PackPersistentData.isOpened = true;
        }

        protected override void Execute(IPopupViewModel parameter)
        {
            var levels = _packRepository.GetLevelsForPack(_packGameData.PackPersistentData);
            _gameDataProvider.Update(new GameData(_packGameData, levels));
            var command = new NextLevelControlCommand(_game, _energyManager, _gameDataProvider, _popupManager, _levelRepository);
            command.Execute(parameter);
        }
    }
}