using Common.Configurations.Packs;

namespace Common.Data.Models
{
    public class GameData
    {
        public GameData(PackConfiguration packConfiguration, PackLevelCollection packLevelCollection, LevelPreviewData levelPreviewData)
        {
            PackConfiguration = packConfiguration;
            PackLevelCollection = packLevelCollection;
            LevelPreviewData = levelPreviewData;
        }

        public LevelPreviewData LevelPreviewData { get; }
        public PackLevelCollection PackLevelCollection { get; }
        public PackConfiguration PackConfiguration { get; }
    }
}