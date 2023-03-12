using System.Collections.Generic;
using Game.Behaviors;
using Game.Blocks.Behaviors.ChainBomb.Insfrastructure;
using Game.Field;
using UnityEngine;

namespace Game.Blocks.Behaviors.ChainBomb
{
    public class ChainBombDestroyBehavior : IObjectBehavior<Block>
    {
        private readonly GameField _gameField;
        private readonly ChainBombConfiguration _chainBombConfiguration;

        private static readonly List<FieldPosition> MoveDirections = new List<FieldPosition>
        {
            FieldPosition.RightDirection, 
            FieldPosition.LeftDirection, 
            FieldPosition.DownDirection, 
            FieldPosition.UpDirection,
        };

        public ChainBombDestroyBehavior(GameField gameField, ChainBombConfiguration chainBombConfiguration)
        {
            _gameField = gameField;
            _chainBombConfiguration = chainBombConfiguration;
        }
        
        public void Behave(Block entity, Collision2D collision2D)
        {
            var startPosition = _gameField.GetBlockPosition(entity);
            
            if (startPosition == FieldPosition.None)
            {
                return;
            }

            var longestChain = FindLongestChain(startPosition);
            ExecuteChain(longestChain);
        }

        private void ExecuteChain(List<FieldPosition> chainPositions)
        {
            foreach (var position in chainPositions)
            {
                var block = _gameField[position];
                block.DestroyWithTag(_chainBombConfiguration.ColliderTag.Tag);
            }
        }

        private List<FieldPosition> FindLongestChain(in FieldPosition startPosition)
        {
            var maxCount = 0;
            var result = new List<FieldPosition>();
            
            result = TryUpdatePositionLongestChain(startPosition.Up(), ref maxCount, result);
            result = TryUpdatePositionLongestChain(startPosition.Down(), ref maxCount, result);
            result = TryUpdatePositionLongestChain(startPosition.Left(), ref maxCount, result);
            result = TryUpdatePositionLongestChain(startPosition.Right(), ref maxCount, result);

            return result;
        }

        private List<FieldPosition> TryUpdatePositionLongestChain(in FieldPosition positionToCheck, ref int currentMaxCount, 
            List<FieldPosition> defaultPositions)
        {
            var chainPositions = GetChainPositions(positionToCheck);

            if (chainPositions.Count > currentMaxCount)
            {
                currentMaxCount = chainPositions.Count;
                return chainPositions;
            }

            return defaultPositions;
        }
        
        private List<FieldPosition> GetChainPositions(in FieldPosition startPosition)
        {
            if (_gameField.TryGetBlock(startPosition, out var startBlock) == false)
            {
                return new List<FieldPosition>();
            }
            
            var chainPointsQueue = new HashQueue<FieldPosition>();
            var startBlockId = startBlock.BlockConfiguration.BlockId;
            
            chainPointsQueue.Enqueue(startPosition);
            
            while (chainPointsQueue.Any())
            {
                var currentPoint = chainPointsQueue.Dequeue();
                
                foreach (var moveDirection in MoveDirections)
                {
                    var nextPoint = moveDirection + currentPoint;

                    if (_gameField.TryGetBlock(nextPoint, out var block) && block.BlockConfiguration.BlockId == startBlockId)
                    {
                        chainPointsQueue.Enqueue(nextPoint);
                    }
                }
            }

            return chainPointsQueue.ToList();
        }
    }
}