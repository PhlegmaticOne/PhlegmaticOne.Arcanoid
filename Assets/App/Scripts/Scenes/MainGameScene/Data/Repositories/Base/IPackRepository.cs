using System.Collections.Generic;
using Scenes.MainGameScene.Configurations.Packs;

namespace Scenes.MainGameScene.Data.Repositories.Base
{
    public interface IPackRepository
    {
        IEnumerable<PackConfiguration> GetAll();
        PackLevelCollection GetLevels(string packName, bool resetIfInitialized = false);
        int GetLevelsCount(string packName);
        void Save(PackLevelCollection packLevelCollection);
        void Save(PackConfiguration packConfiguration);
    }
}