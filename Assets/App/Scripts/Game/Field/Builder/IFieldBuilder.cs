using Common.Data.Models;

namespace Game.Field.Builder
{
    public interface IFieldBuilder
    {
        void BuildField(LevelData levelData);
    }
}