using System.Collections.Generic;
using Common.Configurations.Packs;
using Common.Data.Models;

namespace Common.Data.Repositories.Base
{
    public interface IPackRepository
    {
        DefaultPackConfiguration DefaultPackConfiguration { get; }
        IEnumerable<PackGameData> GetAll();
        PackPersistentData GetPersistentDataForPack(PackConfiguration packConfiguration);
        PackLevelsData GetLevelsForPack(PackPersistentData packPersistentData);
        void Save(PackPersistentData packPersistentData);
    }
}