namespace Common.Packs.Data.Models
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
    }
}