using System.Collections.Generic;
using System.Linq;
using Game.Field;
using Game.GameEntities.Blocks;
using Libs.TimeActions.Base;
using UnityEngine;

namespace Popups.MainGame.Commands
{
    public class WinControlCommandTimeAction : FixedIntervalTimeAction
    {
        private readonly GameField _gameField;
        private readonly List<Block> _activeBlocks;
        private readonly string _tag;

        public WinControlCommandTimeAction(GameField gameField, string tag, float fixedInterval, int actionsCount) :
            base(fixedInterval, actionsCount)
        {
            _gameField = gameField;
            _activeBlocks = gameField.GetActiveBlocks().ToList();
            _tag = tag;
        }

        public override void OnStart()
        {
            foreach (var notActiveBlock in _gameField.GetNotActiveBlocks())
            {
                Destroy(notActiveBlock);
            }
        }

        public override void OnEnd()
        {
            _gameField.Clear();
            _activeBlocks.Clear();
        }

        protected override void OnInterval(int interval)
        {
            if (interval < 0 || interval >= _activeBlocks.Count)
            {
                return;
            }
            
            var block = _activeBlocks[interval];
            
            if (block != null && block.IsDestroyed == false)
            {
                Destroy(block);
            }
        }

        private void Destroy(Block block)
        {
            block.OnDestroyBehaviors.RemoveNotDefaultBehaviors(_tag);
            block.DestroyWithTag(_tag, null);
        }
    }
}