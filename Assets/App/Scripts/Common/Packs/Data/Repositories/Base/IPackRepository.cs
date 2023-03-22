using System.Collections.Generic;
using Common.Packs.Configurations;
using Common.Packs.Data.Models;

namespace Common.Packs.Data.Repositories.Base
{
    public interface IPackRepository
    {
        DefaultPackConfiguration DefaultPackConfiguration { get; }
        IEnumerable<PackGameData> GetAll();
        PackConfiguration GetNextPackConfiguration(PackConfiguration current);
        PackPersistentData GetPersistentDataForPack(PackConfiguration packConfiguration);
        PackLevelsData GetLevelsForPack(PackPersistentData packPersistentData);
        void Save(PackPersistentData packPersistentData);
    }
}