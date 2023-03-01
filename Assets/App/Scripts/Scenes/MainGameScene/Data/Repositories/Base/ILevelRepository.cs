namespace Scenes.MainGameScene.Data.Repositories.Base
{
    public interface ILevelRepository
    {
        LevelData GetLevelData(int levelId, string packName);
    }
}