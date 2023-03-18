﻿using Common.Bag;
using Common.Packs.Data.Models;
using Common.Packs.Data.Repositories.Base;
using Game.Base;
using Game.PopupRequires.Commands.Base;

namespace Game.PopupRequires.Commands
{
    public class StartGameCommand : ICommand
    {
        private readonly IObjectBag _objectBag;
        private readonly ILevelRepository _levelRepository;
        private readonly IGame<MainGameData, MainGameEvents> _mainGame;

        public StartGameCommand(IObjectBag objectBag,
            ILevelRepository levelRepository,
            IGame<MainGameData, MainGameEvents> mainGame)
        {
            _objectBag = objectBag;
            _levelRepository = levelRepository;
            _mainGame = mainGame;
        }
        
        public void Execute()
        {
            var gameData = _objectBag.Get<GameData>();
            var levelData = _levelRepository.GetLevelData(gameData.PackGameData.PackPersistentData);
            _objectBag.Set(levelData);
            _mainGame.StartGame(new MainGameData(levelData));
        }
    }
}