using Common.Bag;
using Common.Energy;
using Common.Packs.Data.Models;
using Common.Packs.Data.Repositories.Base;
using Game;
using Game.Base;
using Libs.Popups.ViewModels.Commands;

namespace Popups.MainGame.Commands
{
    public class MainGamePopupOnShowCommand : EmptyCommandBase
    {
        private readonly IGame<MainGameData, MainGameEvents> _game;
        private readonly IObjectBag _objectBag;
        private readonly ILevelRepository _levelRepository;

        public MainGamePopupOnShowCommand(IGame<MainGameData, MainGameEvents> game,
            IObjectBag objectBag,
            ILevelRepository levelRepository)
        {
            _game = game;
            _objectBag = objectBag;
            _levelRepository = levelRepository;
        }
        
        protected override void Execute()
        {
            var gameData = _objectBag.Get<GameData>();
            var levelData = _levelRepository.GetLevelData(gameData.PackGameData.PackPersistentData);
            _objectBag.Set(levelData);
            _game.StartGame(new MainGameData(levelData));
        }
    }
}