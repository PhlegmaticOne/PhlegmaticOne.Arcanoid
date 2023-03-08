using Common.Data.Models;

namespace Common.Data.Providers
{
    public class GameDataProvider
    {
        private GameData _gameData;

        public void Set(GameData gameData) => _gameData = gameData;
        
        public GameData Get() => _gameData;
    }
}