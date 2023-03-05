using Game.Blocks.Spawners;
using Game.Field.Builder;
using Game.Field.Helpers;
using UnityEngine;

namespace Game.Field.Installer
{
    public class FieldInstaller : MonoBehaviour
    {
        [SerializeField] private FieldPositionsGenerator _fieldPositionsGenerator;
        [SerializeField] private GameField _gameField;

        public Bounds GetFieldBounds() => _fieldPositionsGenerator.GenerateFieldBounds();

        public IFieldBuilder CreateFieldBuilder(IBlockSpawner blockSpawner)
        {
            return new FieldBuilder(blockSpawner, _fieldPositionsGenerator, _gameField);
        }
    }
}