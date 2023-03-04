using System.Collections.Generic;
using Common.Data.Models;
using Game.Blocks;
using Game.Blocks.Spawners;
using Game.Field.Configurations;
using Game.Field.Helpers;
using UnityEngine;

namespace Game.Field.Builder
{
    public class FieldBuilder : MonoBehaviour, IFieldBuilder
    {
        [SerializeField] private FieldPositionsGenerator _fieldPositionsGenerator;
        [SerializeField] private GameFieldConfiguration _gameFieldConfiguration;
        [SerializeField] private GameField _gameField;
        [SerializeField] private Block _block;
        private IBlockSpawner _blockSpawner;

        public void Initialize(IBlockSpawner blockSpawner)
        {
            _blockSpawner = blockSpawner;
        }

        public void BuildField(LevelData levelData)
        {
            var width = levelData.Width;
            var height = levelData.Height;
            var blocksData = levelData.BlocksData;
            var positionsGenerationResult = _fieldPositionsGenerator.GeneratePositions(
                _gameFieldConfiguration.FieldMargin,
                new Vector2(_gameFieldConfiguration.BlockMarginRight, _gameFieldConfiguration.BlockMarginTop),
                new Vector2Int(width, height),
                _block.GetBaseHeight());
            var positions = positionsGenerationResult.CellPositions;
            var cellSize = positionsGenerationResult.CellSize;

            var result = new List<Block>();
            
            for (var y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var blockData = blocksData[y * levelData.Width + x];
                    var position = positions[y, x];
                    var block = _blockSpawner.SpawnBlock(blockData, new BlockSpawnData(cellSize, position));
                    result.Add(block);
                }
            }
            
            _gameField.Initialize(result, width, height);
        }
    }
}