using System.Collections.Generic;
using Game.Field;
using Game.GameEntities.Blocks.Configurations;
using UnityEngine;

namespace Game.GameEntities.Blocks.Behaviors.Bombs.Common.AffectStrategies
{
    public class BlockAffectStrategy : IBlockAffectStrategy
    {
        private readonly BombConfiguration _bombConfiguration;
        private readonly GameField _gameField;

        public BlockAffectStrategy(BombConfiguration bombConfiguration, GameField gameField)
        {
            _bombConfiguration = bombConfiguration;
            _gameField = gameField;
        }
        
        public void AffectBlockAtPosition(FieldPosition fieldPosition, Collision2D original)
        {
            var block = GetBlockAtPosition(fieldPosition);

            if (block == null)
            {
                return;
            }

            if (_bombConfiguration.BlockAffecting == BlockAffectingType.BothAndSeparate)
            {
                if (CheckConfiguration(_bombConfiguration.DamageAffectsOnBlocks, block))
                {
                    TryDamageBlock(block, original);
                    return;
                }

                if (CheckConfiguration(_bombConfiguration.DestroyAffectsOnBlocks, block))
                {
                    DestroyBlock(block, original);
                    return;
                }
            }

            if (_bombConfiguration.IsAffectsOnAllBlocks &&
                _bombConfiguration.BlockAffecting == BlockAffectingType.Damage)
            {
                TryDamageBlock(block, original);
                return;
            }

            if (_bombConfiguration.IsAffectsOnAllBlocks &&
                _bombConfiguration.BlockAffecting == BlockAffectingType.Destroying)
            {
                DestroyBlock(block, original);
                return;
            }
                
            if (CheckAffectingTypeAndConfiguration(BlockAffectingType.Damage, 
                    _bombConfiguration.DamageAffectsOnBlocks, block))
            {
                TryDamageBlock(block, original);
                return;
            }

            if (CheckAffectingTypeAndConfiguration(BlockAffectingType.Destroying,
                    _bombConfiguration.DestroyAffectsOnBlocks, block))
            {
                DestroyBlock(block, original);
            }
        }
        
        private void TryDamageBlock(Block block, Collision2D original)
        {
            if (block.CurrentHealth > _bombConfiguration.RemovesLifesCount)
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
            for (var i = 0; i < _bombConfiguration.RemovesLifesCount; i++)
            {
                block.CollideWithTag(_bombConfiguration.ColliderTag.Tag, original);
            }
        }
        
        private void DestroyBlock(Block block, Collision2D original)
        {
            block.DestroyWithTag(_bombConfiguration.ColliderTag.Tag, original);
        }

        private Block GetBlockAtPosition(in FieldPosition fieldPosition)
        {
            return _gameField.TryGetBlock(fieldPosition, out var block) == false || block.IsDestroyed ? null : block;
        }

        protected bool CheckAffectingTypeAndConfiguration(BlockAffectingType blockAffectingType,
            List<BlockConfiguration> blockConfigurations, Block block) =>
            _bombConfiguration.BlockAffecting == blockAffectingType &&
            CheckConfiguration(blockConfigurations, block);
        
        private static bool CheckConfiguration(List<BlockConfiguration> blockConfigurations, Block block) =>
            blockConfigurations.Contains(block.BlockConfiguration);
    }
}