using Common.Packs.Data.Models;

namespace Common.Providers
{
    public interface IGameDataProvider
    {
        GameData GetGameData();
        void Update(GameData gameData);
    }
}