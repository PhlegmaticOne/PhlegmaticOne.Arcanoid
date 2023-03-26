using Common.Game.Providers;
using Common.Packs.Data.Models;

namespace Common.Game.Providers.Providers
{
    public class GameDataProvider : IGameDataProvider
    {
        private GameData _gameData;
        public GameData GetGameData() => _gameData;
        public void SetNewLevel(LevelData levelData) => _gameData.SetNewLevel(levelData);
        public void Update(GameData gameData) => _gameData = gameData;
    }
}