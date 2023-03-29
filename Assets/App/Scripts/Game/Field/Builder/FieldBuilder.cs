using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Packs.Data.Models;
using DG.Tweening;
using Game.Common;
using Game.Field.Helpers;
using Game.GameEntities.Blocks;
using Game.GameEntities.Blocks.Spawners;
using UnityEngine;

namespace Game.Field.Builder
{
    public class FieldBuilder : IFieldBuilder
    {
        private readonly IBlockSpawner _blockSpawner;
        private readonly FieldPositionsGenerator _fieldPositionsGenerator;
        private readonly GameField _gameField;
        private readonly Transform _pointTransform;
        private readonly FieldBuilderInfo _fieldBuilderInfo;

        public event Action FieldBuilt;
        
        public FieldBuilder(IBlockSpawner blockSpawner, 
            FieldPositionsGenerator fieldPositionsGenerator,
            GameField gameField,
            Transform pointTransform,
            FieldBuilderInfo fieldBuilderInfo)
        {
            _blockSpawner = blockSpawner;
            _fieldPositionsGenerator = fieldPositionsGenerator;
            _gameField = gameField;
            _pointTransform = pointTransform;
            _fieldBuilderInfo = fieldBuilderInfo;
        }

        public GameField BuildField(LevelData levelData)
        {
            var width = levelData.Width;
            var height = levelData.Height;
            var positionsGenerationResult = _fieldPositionsGenerator
                .GeneratePositions(new Vector2Int(width, height));
            var blocks = GetBlocks(levelData,
                new BlockSpawnData(positionsGenerationResult.CellSize, _pointTransform.position));

            _gameField.Initialize(width, height, blocks);
            BuildAsync(blocks, positionsGenerationResult.CellPositions, width, height);
            return _gameField;
        }
        
        private async void BuildAsync(List<Block> blocks, Vector2[,] positions, int width, int height)
        {
            var interval = _fieldBuilderInfo.ScalePunchTime;
            var wait = (int)(_fieldBuilderInfo.GetIntervalTime(width + height - 1) * 1000);
            var totalTasks = new List<Task>();

            for (var row = 0; row < height; row++)
            {
                for (var col = 0; col < width; col++)
                {
                    var r = row - col;

                    if (r < 0)
                    {
                        break;
                    }
                    
                    TryAddTask(totalTasks, blocks, positions, width, r, col, interval);
                }
                
                await Task.Delay(wait);
            }
            
            for (var col = 1; col < width; col++)
            {
                for (var row = height - 1; row >= 0; row--)
                {
                    var c = col + (height - row - 1);
                    if (c >= width)
                    {
                        break;
                    }
                    
                    TryAddTask(totalTasks, blocks, positions, width, row, c, interval);
                }
            
                await Task.Delay(wait);
            }

            await Task.WhenAll(totalTasks);
            FieldBuilt?.Invoke();
        }

        private void TryAddTask(List<Task> tasks, List<Block> blocks, Vector2[,] positions,
            in int width, in int row, in int col, in float interval)
        {
            var block = blocks[row * width + col];
            if (block == null)
            {
                return;
            }
            
            var position = positions[row, col];
            var transform = block.transform;
            transform.position = position;
            transform.localScale = Vector3.zero;
            tasks.Add(block.transform.DOScale(Vector3.one, interval)
                .SetEase(Ease.OutElastic)
                .Play()
                .AsyncWaitForCompletion());
        }

        private List<Block> GetBlocks(LevelData levelData, BlockSpawnData blockSpawnData)
        {
            var result = new List<Block>();
            var width = levelData.Width;
            var height = levelData.Height;
            var blocksData = levelData.BlocksData;
            
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var blockData = blocksData[y * levelData.Width + x];
                    var block = _blockSpawner.SpawnBlock(blockData, blockSpawnData);
                    result.Add(block);
                }
            }

            return result;
        }
    }
}