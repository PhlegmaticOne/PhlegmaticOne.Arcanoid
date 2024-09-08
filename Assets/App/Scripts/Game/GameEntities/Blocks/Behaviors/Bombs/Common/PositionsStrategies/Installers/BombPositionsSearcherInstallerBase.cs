using Game.Field;
using UnityEngine;

namespace Game.GameEntities.Blocks.Behaviors.Bombs.Common.PositionsStrategies.Installers
{
    public abstract class BombPositionsSearcherInstallerBase : MonoBehaviour
    {
        public abstract IBombPositionsSearcher CreateBombPositionsSearcher(BombConfiguration bombConfiguration,
            GameField gameField);
    }
}