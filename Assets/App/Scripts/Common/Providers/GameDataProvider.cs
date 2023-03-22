using Common.Packs.Data.Models;

namespace Common.Providers
{
    public class GameDataProvider : IGameDataProvider
    {
        private GameData _gameData;
        
        public GameData GetGameData() => _gameData;

        public void Update(GameData gameData) => _gameData = gameData;
    }
}