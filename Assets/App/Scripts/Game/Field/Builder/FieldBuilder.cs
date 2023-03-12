using System.Collections.Generic;
using Common.Data.Models;
using Game.Blocks;
using Game.Blocks.Spawners;
using Game.Field.Helpers;
using UnityEngine;

namespace Game.Field.Builder
{
    public class FieldBuilder : IFieldBuilder
    {
        private readonly IBlockSpawner _blockSpawner;
        private readonly FieldPositionsGenerator _fieldPositionsGenerator;
        private readonly GameField _gameField;

        public FieldBuilder(IBlockSpawner blockSpawner, 
            FieldPositionsGenerator fieldPositionsGenerator,
            GameField gameField)
        {
            _blockSpawner = blockSpawner;
            _fieldPositionsGenerator = fieldPositionsGenerator;
            _gameField = gameField;
        }

        public GameField BuildField(LevelData levelData)
        {
            var width = levelData.Width;
            var height = levelData.Height;
            var blocksData = levelData.BlocksData;
            var positionsGenerationResult = _fieldPositionsGenerator.GeneratePositions(new Vector2Int(width, height));
            
            var positions = positionsGenerationResult.CellPositions;
            var cellSize = positionsGenerationResult.CellSize;

            var result = new List<Block>();
            
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var blockData = blocksData[y * levelData.Width + x];
                    var position = positions[y, x];
                    var block = _blockSpawner.SpawnBlock(blockData, new BlockSpawnData(cellSize, position));
                    result.Add(block);
                }
            }
            
            _gameField.Initialize(width, height, result);
            return _gameField;
        }
    }
}