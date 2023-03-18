using Game.Field;
using Game.GameEntities.Blocks.Behaviors.Bombs.Common.TimeActions.Base;
using UnityEngine;

namespace Game.GameEntities.Blocks.Behaviors.Bombs.Common.TimeActions.Installer
{
    public abstract class BombTimeActionInstallerBase : MonoBehaviour
    {
        public abstract IBombTimeAction CreateBombTimeAction(BombConfiguration bombConfiguration,
            GameField gameField);
    }
}