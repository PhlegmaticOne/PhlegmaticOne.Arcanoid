using System;
using System.Collections.Generic;
using System.Linq;
using Common.Packs.Data.Models;
using DG.Tweening;
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
        private readonly float _transitionTime;

        public FieldBuilder(IBlockSpawner blockSpawner, 
            FieldPositionsGenerator fieldPositionsGenerator,
            GameField gameField,
            Transform pointTransform,
            float transitionTime)
        {
            _blockSpawner = blockSpawner;
            _fieldPositionsGenerator = fieldPositionsGenerator;
            _gameField = gameField;
            _pointTransform = pointTransform;
            _transitionTime = transitionTime;
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
            
            var transitionTime = _transitionTime / blocks.Count(x => x != null);
            var sequence = DOTween.Sequence();
            var current = 0;

            for (var i = 0; i < blocks.Count; i++)
            {
                var block = blocks[i];
                var finalPosition = positions[current / width, i % width];

                if (block != null)
                {
                    sequence.Append(block.transform.DOMove(finalPosition, transitionTime));
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