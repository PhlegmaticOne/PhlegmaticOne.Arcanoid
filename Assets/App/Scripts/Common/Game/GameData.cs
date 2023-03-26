using Common.Packs.Data.Models;

namespace Common.Game.Providers
{
    public class GameData
    {
        public GameData(PackGameData packGameData, PackLevelsData packLevelsData)
        {
            PackLevelsData = packLevelsData;
            PackGameData = packGameData;
        }

        public PackLevelsData PackLevelsData { get; }
        public PackGameData PackGameData { get; }
        public LevelData CurrentLevel { get; private set; }
        public void SetNewLevel(LevelData levelData) => CurrentLevel = levelData;
    }
}