using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly DynamicBlockAffectingInfo _dynamicBlockAffectingInfo;
        private readonly int _maxBlocks;

        public FieldBuilder(IBlockSpawner blockSpawner, 
            FieldPositionsGenerator fieldPositionsGenerator,
            GameField gameField,
            Transform pointTransform,
            DynamicBlockAffectingInfo dynamicBlockAffectingInfo)
        {
            _blockSpawner = blockSpawner;
            _fieldPositionsGenerator = fieldPositionsGenerator;
            _gameField = gameField;
            _pointTransform = pointTransform;
            _dynamicBlockAffectingInfo = dynamicBlockAffectingInfo;
            _maxBlocks = (int)(dynamicBlockAffectingInfo.MaxBuildingTime / dynamicBlockAffectingInfo.Interval);
        }

        public GameField BuildField(LevelData levelData)
        {
            var width = levelData.Width;
            var height = levelData.Height;
            var positionsGenerationResult = _fieldPositionsGenerator
                .GeneratePositions(new Vector2Int(width, height));
            var positions = positionsGenerationResult.CellPositions;
            var blocks = GetBlocks(levelData,
                new BlockSpawnData(positionsGenerationResult.CellSize, _pointTransform.position));

            _gameField.Initialize(width, height, blocks);

            var actionsCount = blocks.Count(x => x != null);
            var interval = _dynamicBlockAffectingInfo.GetAffectingInterval(actionsCount);
            var sequence = DOTween.Sequence();
            var current = 0;

            for (var i = 0; i < blocks.Count; i++)
            {
                var block = blocks[i];
                var finalPosition = positions[current / width, i % width];

                if (block != null)
                {
                    sequence.Append(block.transform.DOMove(finalPosition, interval));
                }

                current++;
            }

            sequence.Play().OnComplete(() => FieldBuilt?.Invoke());
            return _gameField;
        }

        public event Action FieldBuilt;

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