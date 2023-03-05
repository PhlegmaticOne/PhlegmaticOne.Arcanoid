﻿using Game.Field.Configurations;
using UnityEngine;

namespace Game.Field.Helpers
{
    public class FieldPositionsGenerator : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        private float _screenHeight;
        private float _screenWidth;

        private void Start()
        {
            _screenHeight = Screen.height;
            _screenWidth = Screen.width;
        }

        public FieldPositionsGenerationResult GeneratePositions(MarginInPercentage fieldMargin, Vector2 itemMargins,
            Vector2Int fieldSize, float baseItemHeight)
        {
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
            
            if (cellSizeWorld.y > baseItemHeight)
            {
                cellSizeWorld.y = baseItemHeight;
            }
            
            var marginRightWorld = ToWorldSize(new Vector2(blockMarginRight, 0)).magnitude;
            var marginTopWorld = ToWorldSize(new Vector2(0, blockMarginTop)).magnitude;


            var startPositionScreen = new Vector2(leftMargin, _screenHeight - topMargin);
            var endPositionScreen = new Vector2(_screenWidth - rightMargin, bottomMargin);
            
            var fieldStartPosition = ToWorldPoint(startPositionScreen);
            var fieldEndPosition = ToWorldPoint(endPositionScreen);
            
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

            var fieldSizeWorld = new Vector2(
                fieldEndPosition.x - fieldStartPosition.x,
                fieldStartPosition.y - fieldEndPosition.y);

            var fieldBounds = new Bounds((fieldEndPosition + fieldStartPosition) / 2, fieldSizeWorld);

            return new FieldPositionsGenerationResult(result, cellSizeWorld, fieldBounds);
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
        public FieldPositionsGenerationResult(Vector2[,] cellPositions, Vector2 cellSize, Bounds fieldBounds)
        {
            CellPositions = cellPositions;
            CellSize = cellSize;
            FieldBounds = fieldBounds;
        }

        public Bounds FieldBounds { get; }
        public Vector2 CellSize { get; }
        public Vector2[,] CellPositions { get; }
    }
}