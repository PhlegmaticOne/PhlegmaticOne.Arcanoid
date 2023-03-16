using System.Collections.Generic;
using Game.Field;
using Game.GameEntities.Blocks.Behaviors.ChainBomb;
using Game.GameEntities.Blocks.Configurations;
using Libs.Behaviors;
using UnityEngine;

namespace Game.GameEntities.Blocks.Behaviors.Bombs.Common
{
    public abstract class BombBehaviorBase : IObjectBehavior<Block>
    {
        protected readonly GameField GameField;
        protected readonly BombConfiguration BombConfiguration;

        protected BombBehaviorBase(GameField gameField, BombConfiguration bombBlockConfiguration)
        {
            GameField = gameField;
            BombConfiguration = bombBlockConfiguration;
        }
        
        public void Behave(Block entity, Collision2D collision2D)
        {
            var bombPosition = GameField.GetBlockPosition(entity);

            if (bombPosition == FieldPosition.None)
            {
                return;
            }

            var positions = GetAffectingPositions(bombPosition);
            ApplyBombToPositions(positions, collision2D);
        }

        protected abstract List<FieldPosition> GetAffectingPositions(in FieldPosition bombPosition);

        protected virtual void ApplyBombToPositions(List<FieldPosition> positions, Collision2D original)
        {
            foreach (var fieldPosition in positions)
            {
                var block = GetBlockAtPosition(fieldPosition);

                if (block == null)
                {
                    continue;
                }

                if (BombConfiguration.BlockAffecting == BlockAffectingType.BothAndSeparate)
                {
                    if (CheckConfiguration(BombConfiguration.DamageAffectsOnBlocks, block))
                    {
                        TryDamageBlock(block, original);
                        continue;
                    }

                    if (CheckConfiguration(BombConfiguration.DestroyAffectsOnBlocks, block))
                    {
                        DestroyBlock(block, original);
                        continue;
                    }
                }

                if (BombConfiguration.IsAffectsOnAllBlocks &&
                    BombConfiguration.BlockAffecting == BlockAffectingType.Damage)
                {
                    TryDamageBlock(block, original);
                    continue;
                }

                if (BombConfiguration.IsAffectsOnAllBlocks &&
                    BombConfiguration.BlockAffecting == BlockAffectingType.Destroying)
                {
                    DestroyBlock(block, original);
                    continue;
                }
                
                if (CheckAffectingTypeAndConfiguration(BlockAffectingType.Damage, 
                        BombConfiguration.DamageAffectsOnBlocks, block))
                {
                    TryDamageBlock(block, original);
                    continue;
                }

                if (CheckAffectingTypeAndConfiguration(BlockAffectingType.Destroying,
                        BombConfiguration.DestroyAffectsOnBlocks, block))
                {
                    DestroyBlock(block, original);
                }
            }
        }

        private void TryDamageBlock(Block block, Collision2D original)
        {
            if (block.CurrentHealth > BombConfiguration.RemovesLifesCount)
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
            for (var i = 0; i < BombConfiguration.RemovesLifesCount; i++)
            {
                block.CollideWithTag(BombConfiguration.ColliderTag.Tag, original);
            }
        }
        
        private void DestroyBlock(Block block, Collision2D original)
        {
            block.DestroyWithTag(BombConfiguration.ColliderTag.Tag, original);
        }

        protected Block GetBlockAtPosition(in FieldPosition fieldPosition)
        {
            return GameField.TryGetBlock(fieldPosition, out var block) == false || block.IsDestroyed ? null : block;
        }
        
        protected bool CheckConfiguration(List<BlockConfiguration> blockConfigurations, Block block) =>
            blockConfigurations.Contains(block.BlockConfiguration);
        
        protected bool CheckAffectingTypeAndConfiguration(BlockAffectingType blockAffectingType,
            List<BlockConfiguration> blockConfigurations, Block block) =>
            BombConfiguration.BlockAffecting == blockAffectingType &&
            CheckConfiguration(blockConfigurations, block);
    }
}