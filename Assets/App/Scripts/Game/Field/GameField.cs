using Game.Blocks.View;
using Game.Field.Configurations;
using Game.Field.Helpers;
using UnityEngine;

namespace Game.Field
{
    public class GameField : MonoBehaviour
    {
        [SerializeField] private GameFieldConfiguration _gameFieldConfiguration;
        [SerializeField] private FieldPositionsGenerator _fieldPositionsGenerator;
        [SerializeField] private BlockView _blockView;

        [SerializeField] private int _columnsCount = 4;
        [SerializeField] private int _rowsCount;
        
        public void GenerateBlocks()
        {
            var generationResult = _fieldPositionsGenerator.GeneratePositions(
                _gameFieldConfiguration.FieldMargin,
                new Vector2(_gameFieldConfiguration.BlockMarginRight, _gameFieldConfiguration.BlockMarginTop),
                new Vector2Int(_columnsCount, _rowsCount),
                _blockView.BaseWorldHeight);
            
            var positions = generationResult.CellPositions;
            var cellSize = generationResult.CellSize;
            
            for (var y = 0; y < positions.GetLength(0); y++)
            {
                for (var x = 0; x < positions.GetLength(1); x++)
                {
                    SpawnBlock(positions[y, x], ref cellSize);
                }
            }
        }

        private void SpawnBlock(Vector2 position, ref Vector2 size)
        {
            var view = Instantiate(_blockView, transform.parent);
            view.SetSize(size);
            view.transform.position = position;
        }
    }
}