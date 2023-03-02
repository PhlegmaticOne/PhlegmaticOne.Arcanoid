namespace Scenes.MainGameScene.Data.Repositories.Base
{
    public interface ILevelRepository
    {
        LevelData GetLevelData(LevelPreviewData levelPreviewData);
    }
}