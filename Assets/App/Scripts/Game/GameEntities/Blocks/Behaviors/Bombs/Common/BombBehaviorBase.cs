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
            ApplyBombToPositions(positions);
        }

        protected abstract List<FieldPosition> GetAffectingPositions(in FieldPosition bombPosition);

        protected virtual void ApplyBombToPositions(List<FieldPosition> positions)
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
                        TryDamageBlock(block);
                        continue;
                    }

                    if (CheckConfiguration(BombConfiguration.DestroyAffectsOnBlocks, block))
                    {
                        DestroyBlock(block);
                        continue;
                    }
                }

                if (BombConfiguration.IsAffectsOnAllBlocks &&
                    BombConfiguration.BlockAffecting == BlockAffectingType.Damage)
                {
                    TryDamageBlock(block);
                    continue;
                }

                if (BombConfiguration.IsAffectsOnAllBlocks &&
                    BombConfiguration.BlockAffecting == BlockAffectingType.Destroying)
                {
                    DestroyBlock(block);
                    continue;
                }
                
                if (CheckAffectingTypeAndConfiguration(BlockAffectingType.Damage, 
                        BombConfiguration.DamageAffectsOnBlocks, block))
                {
                    TryDamageBlock(block);
                    continue;
                }

                if (CheckAffectingTypeAndConfiguration(BlockAffectingType.Destroying,
                        BombConfiguration.DestroyAffectsOnBlocks, block))
                {
                    DestroyBlock(block);
                }
            }
        }

        private void TryDamageBlock(Block block)
        {
            if (block.CurrentHealth > BombConfiguration.RemovesLifesCount)
            {
                DamageBlock(block);
            }
            else
            {
                DestroyBlock(block);
            }
        }

        private void DamageBlock(Block block)
        {
            for (var i = 0; i < BombConfiguration.RemovesLifesCount; i++)
            {
                block.CollideWithTag(BombConfiguration.ColliderTag.Tag);
            }
        }
        
        private void DestroyBlock(Block block)
        {
            block.DestroyWithTag(BombConfiguration.ColliderTag.Tag);
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