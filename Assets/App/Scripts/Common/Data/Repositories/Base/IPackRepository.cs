using System.Collections.Generic;
using Common.Configurations.Packs;

namespace Common.Data.Repositories.Base
{
    public interface IPackRepository
    {
        bool PacksInitialized { get; }
        DefaultPackConfiguration DefaultPackConfiguration { get; }
        IEnumerable<PackConfiguration> GetAll();
        PackLevelCollection GetLevels(string packName);
        int GetLevelsCount(string packName);
        void Save(PackLevelCollection packLevelCollection);
        void Save(PackConfiguration packConfiguration);
        void MarkAsInitialized();
        void Save();
    }
}