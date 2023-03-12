using Game.Behaviors;
using Game.Field;
using UnityEngine;

namespace Game.Blocks.Behaviors.Bomb
{
    public class BombBlocksDestroyBehavior : IObjectBehavior<Block>
    {
        private readonly GameField _gameField;
        private readonly BombBlockConfiguration _bombBlockConfiguration;

        public BombBlocksDestroyBehavior(GameField gameField, BombBlockConfiguration bombBlockConfiguration)
        {
            _gameField = gameField;
            _bombBlockConfiguration = bombBlockConfiguration;
        }
        
        public void Behave(Block entity, Collision2D collision2D)
        {
            var blockPosition = _gameField.GetBlockPosition(entity);
            
            if (blockPosition == FieldPosition.None)
            {
                return;
            }

            for (var currentRadius = 1; currentRadius <= _bombBlockConfiguration.DestroyingRadius; ++currentRadius)
            {
                CollideBlockAtPosition(blockPosition.Up(currentRadius));
                CollideBlockAtPosition(blockPosition.Down(currentRadius));
                CollideBlockAtPosition(blockPosition.Left(currentRadius));
                CollideBlockAtPosition(blockPosition.Right(currentRadius));
                CollideBlockAtPosition(blockPosition.LeftUp(currentRadius));
                CollideBlockAtPosition(blockPosition.RightUp(currentRadius));
                CollideBlockAtPosition(blockPosition.LeftDown(currentRadius));
                CollideBlockAtPosition(blockPosition.RightDown(currentRadius));
            }
        }

        private void CollideBlockAtPosition(in FieldPosition fieldPosition)
        {
            if (_gameField.ContainsPosition(fieldPosition) == false)
            {
                return;
            }

            if (_gameField.TryGetBlock(fieldPosition, out var block) == false || block.IsDestroyed)
            {
                return;
            }

            if (_bombBlockConfiguration.DamagesBlocks &&
                _bombBlockConfiguration.DamageAffectsOnBlocks.Contains(block.BlockConfiguration))
            {
                if (block.CurrentHealth >= _bombBlockConfiguration.RemovesLifesCount)
                {
                    DamageBlock(block);
                }
                else
                {
                    DestroyBlock(block);
                }
                return;
            }

            if (_bombBlockConfiguration.DestroysBlocks &&
                _bombBlockConfiguration.DestroyAffectsOnBlocks.Contains(block.BlockConfiguration))
            {
                DestroyBlock(block);
            }
        }

        private void DamageBlock(Block block)
        {
            for (var i = 0; i < _bombBlockConfiguration.RemovesLifesCount; i++)
            {
                block.CollideWithTag(_bombBlockConfiguration.ColliderTag.Tag);
            }
        }
        
        private void DestroyBlock(Block block)
        {
            block.DestroyWithTag(_bombBlockConfiguration.ColliderTag.Tag);
        }
    }
}