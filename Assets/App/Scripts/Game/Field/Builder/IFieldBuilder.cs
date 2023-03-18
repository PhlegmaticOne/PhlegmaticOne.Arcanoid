using Common.Packs.Data.Models;

namespace Game.Field.Builder
{
    public interface IFieldBuilder
    {
        GameField BuildField(LevelData levelData);
    }
}