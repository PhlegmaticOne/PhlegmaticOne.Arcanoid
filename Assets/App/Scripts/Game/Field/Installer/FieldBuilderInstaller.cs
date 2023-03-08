using Game.Blocks.Spawners;
using Game.Field.Builder;
using Game.Field.Helpers;
using UnityEngine;

namespace Game.Field.Installer
{
    public class FieldBuilderInstaller : MonoBehaviour
    {
        [SerializeField] private FieldPositionsGenerator _fieldPositionsGenerator;

        public IFieldBuilder CreateFieldBuilder(IBlockSpawner blockSpawner)
        {
            return new FieldBuilder(blockSpawner, _fieldPositionsGenerator);
        }
    }
}