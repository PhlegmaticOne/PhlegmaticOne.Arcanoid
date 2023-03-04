using Common.Data.Models;
using Game.Base;

namespace Game
{
    public class MainGameData : IGameData
    {
        public MainGameData(LevelData levelData) => LevelData = levelData;
        public LevelData LevelData { get; }
    }
}