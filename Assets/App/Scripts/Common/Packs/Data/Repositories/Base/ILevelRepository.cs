using Common.Packs.Data.Models;

namespace Common.Packs.Data.Repositories.Base
{
    public interface ILevelRepository
    {
        LevelData GetLevelData(PackPersistentData packPersistentData);
    }
}