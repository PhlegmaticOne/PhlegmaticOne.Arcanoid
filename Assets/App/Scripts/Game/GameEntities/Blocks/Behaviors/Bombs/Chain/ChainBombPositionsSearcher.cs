using System.Collections.Generic;
using Game.Field;
using Game.GameEntities.Blocks.Behaviors.Bombs.Chain.Insfrastructure;
using Game.GameEntities.Blocks.Behaviors.Bombs.Common;
using Game.GameEntities.Blocks.Behaviors.Bombs.Common.PositionsStrategies;
using Game.GameEntities.Blocks.Configurations;

namespace Game.GameEntities.Blocks.Behaviors.Bombs.Chain
{
    public class ChainBombPositionsSearcher : IBombPositionsSearcher
    {
        private readonly GameField _gameField;
        private readonly BombConfiguration _bombBlockConfiguration;

        private static readonly List<FieldPosition> MoveDirections = new List<FieldPosition>
        {
            FieldPosition.RightDirection, 
            FieldPosition.LeftDirection, 
            FieldPosition.DownDirection, 
            FieldPosition.UpDirection,
        };

        public ChainBombPositionsSearcher(GameField gameField, BombConfiguration bombBlockConfiguration)
        {
            _gameField = gameField;
            _bombBlockConfiguration = bombBlockConfiguration;
        }
        
        public List<FieldPosition> FindBombAffectingPositions(FieldPosition startPosition) => 
            FindLongestChain(startPosition);

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

            if (CheckAffectingTypeAndConfiguration(BlockAffectingType.Damage,
                    _bombBlockConfiguration.DamageAffectsOnBlocks, startBlock) == false &&
                CheckAffectingTypeAndConfiguration(BlockAffectingType.Destroying,
                    _bombBlockConfiguration.DestroyAffectsOnBlocks, startBlock) == false)
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

        private bool CheckAffectingTypeAndConfiguration(BlockAffectingType blockAffectingType,
            List<BlockConfiguration> blockConfigurations, Block block) =>
            _bombBlockConfiguration.BlockAffecting == blockAffectingType &&
            blockConfigurations.Contains(block.BlockConfiguration);
    }
}