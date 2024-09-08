using Common.Packs.Data.Models;

namespace Common.Game.Providers.Providers
{
    public interface IGameDataProvider
    {
        GameData GetGameData();
        void SetNewLevel(LevelData levelData);
        void Update(GameData gameData);
    }
}