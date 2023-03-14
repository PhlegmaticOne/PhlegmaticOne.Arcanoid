using Common.Data.Models;

namespace Common.Data.Repositories.Base
{
    public interface ILevelRepository
    {
        LevelData GetLevelData(PackPersistentData packPersistentData);
    }
}