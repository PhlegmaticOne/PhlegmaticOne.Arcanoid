using System.Collections.Generic;
using Game.Field;
using Game.GameEntities.Blocks.Configurations;
using Game.GameEntities.Common;
using Game.GameEntities.PlayerObjects.BallObject;
using UnityEngine;

namespace Game.GameEntities.Blocks.Behaviors.Bombs.Common.AffectStrategies
{
    public class BlockAffectStrategy : IBlockAffectStrategy
    {
        private readonly BombConfiguration _bombConfiguration;
        private readonly GameField _gameField;
        private readonly DamageBlockBehavior _damageBlockBehavior;

        public BlockAffectStrategy(BombConfiguration bombConfiguration, GameField gameField)
        {
            _bombConfiguration = bombConfiguration;
            _gameField = gameField;
            _damageBlockBehavior = new DamageBlockBehavior();
        }
        
        public void AffectBlockAtPosition(FieldPosition fieldPosition, Collision2D original)
        {
            var block = GetBlockAtPosition(fieldPosition);

            if (block == null)
            {
                return;
            }

            var affectingType = _bombConfiguration.GetAffectingType(block.BlockConfiguration);

            switch (affectingType)
            {
                case BlockAffectingType.None:
                    return;
                case BlockAffectingType.Damage:
                    TryDamageBlock(block, original);
                    break;
                case BlockAffectingType.Destroying:
                    DestroyBlock(block, original);
                    break;
            }
        }
        
        private void TryDamageBlock(Block block, Collision2D original)
        {
            if (block.CurrentHealth > _bombConfiguration.Damage)
            {
                DamageBlock(block, original);
            }
            else
            {
                DestroyBlock(block, original);
            }
        }

        private void DamageBlock(Block block, Collision2D original)
        {
            _damageBlockBehavior.Damage(block, _bombConfiguration.Damage,
                _bombConfiguration.ColliderTag.Tag, original);
        }
        
        private void DestroyBlock(Block block, Collision2D original)
        {
            block.DestroyWithTag(_bombConfiguration.ColliderTag.Tag, original);
        }

        private Block GetBlockAtPosition(in FieldPosition fieldPosition)
        {
            return _gameField.TryGetBlock(fieldPosition, out var block) == false || block.IsDestroyed ? null : block;
        }
    }
}