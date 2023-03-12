using Game.Blocks;
using Game.Field.Configurations;
using UnityEngine;

namespace Game.Field.Helpers
{
    public class FieldPositionsGenerator : MonoBehaviour
    {
        [SerializeField] private GameFieldConfiguration _gameFieldConfiguration;
        [SerializeField] private Block _block;
        [SerializeField] private Camera _camera;
        private float _screenHeight;
        private float _screenWidth;

        public FieldPositionsGenerationResult GeneratePositions(Vector2Int fieldSize)
        {
            _screenHeight = Screen.height;
            _screenWidth = Screen.width;
            
            var fieldMargin = _gameFieldConfiguration.FieldMargin;
            var itemMargins = new Vector2(_gameFieldConfiguration.BlockMarginRight,
                _gameFieldConfiguration.BlockMarginTop);
            var baseItemSize = _block.GetBaseSize();
            
            var topMargin = HeightFromPercentage(fieldMargin.FromTop);
            var bottomMargin = HeightFromPercentage(fieldMargin.FromBottom);
            var rightMargin = WidthFromPercentage(fieldMargin.FromRight);
            var leftMargin = WidthFromPercentage(fieldMargin.FromLeft);
            
            var blockMarginRight = WidthFromPercentage(itemMargins.x);
            var blockMarginTop = HeightFromPercentage(itemMargins.y);

            var totalBlockHorizontalMargins = (fieldSize.x - 1) * blockMarginRight;
            var totalBlockVerticalMargins = (fieldSize.y - 1) * blockMarginTop;

            var cellWidthScreen = (_screenWidth - totalBlockHorizontalMargins - leftMargin - rightMargin) / fieldSize.x;
            var cellHeightScreen = (_screenHeight - totalBlockVerticalMargins - bottomMargin - topMargin) / fieldSize.y;
            
            var cellSizeWorld = ToWorldSize(new Vector2(cellWidthScreen, cellHeightScreen));

            if (_screenWidth <= _screenHeight)
            {
                var ratio = cellSizeWorld.x / baseItemSize.x;
                cellSizeWorld.y = baseItemSize.y * ratio;
            }

            var marginRightWorld = ToWorldSize(new Vector2(blockMarginRight, 0)).magnitude;
            var marginTopWorld = ToWorldSize(new Vector2(0, blockMarginTop)).magnitude;
            
            var startPositionScreen = new Vector2(leftMargin, _screenHeight - topMargin);
            
            var fieldStartPosition = ToWorldPoint(startPositionScreen);
            
            var startPositionWorld = fieldStartPosition + new Vector2(cellSizeWorld.x / 2, -cellSizeWorld.y / 2);
            var startX = startPositionWorld.x;

            var result = new Vector2[fieldSize.y, fieldSize.x];
            
            for (var y = 0; y < fieldSize.y; y++)
            {
                for (var x = 0; x < fieldSize.x; x++)
                {
                    result[y, x] = startPositionWorld;
                    startPositionWorld += new Vector2(cellSizeWorld.x + marginRightWorld, 0);
                }

                startPositionWorld.x = startX;
                startPositionWorld -= new Vector2(0, cellSizeWorld.y + marginTopWorld);
            }

            return new FieldPositionsGenerationResult(result, cellSizeWorld);
        }

        private float HeightFromPercentage(float percentage) => _screenHeight * percentage;
        private float WidthFromPercentage(float percentage) => _screenWidth * percentage;

        private Vector2 ToWorldPoint(Vector2 vector) => _camera.ScreenToWorldPoint(vector);
        private Vector2 ToWorldSize(Vector2 screenSize)
        {
            var screenPosition = new Vector2(
                screenSize.x + Screen.width / 2.0f,
                screenSize.y + Screen.height / 2.0f);
            return _camera.ScreenToWorldPoint(screenPosition);
        }
    }

    public class FieldPositionsGenerationResult
    {
        public FieldPositionsGenerationResult(Vector2[,] cellPositions, Vector2 cellSize)
        {
            CellPositions = cellPositions;
            CellSize = cellSize;
        }

        public Vector2 CellSize { get; }
        public Vector2[,] CellPositions { get; }
    }
}