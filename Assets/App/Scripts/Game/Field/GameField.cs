﻿using System.Collections.Generic;
using System.Linq;
using Game.Blocks;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Field
{
    public class GameField : MonoBehaviour
    {
        private List<Block> _blocks;
        public event UnityAction<Block> BlockAdded;
        public event UnityAction<Block> BlockRemoved; 
        public int Width { get; private set; }
        public int Height { get; private set; }
        public IReadOnlyList<Block> Blocks => _blocks;
        public int StartActiveBlocksCount { get; private set; }

        public void Initialize(int width, int height, IEnumerable<Block> blocks)
        {
            _blocks = blocks.ToList();
            StartActiveBlocksCount = ActiveBlocksCount;
            Width = width;
            Height = height;
        }

        public int GetBlocksCountOnField(int blockId) =>
            _blocks.Count(x => x != null && x.IsActive && x.IsDestroyed == false && x.BlockConfiguration.BlockId == blockId);

        public Block this[int row, int col] => _blocks[CalculateIndex(row, col)];
        public Block this[in FieldPosition fieldPosition] => _blocks[CalculateIndex(fieldPosition.Row, fieldPosition.Col)];

        public bool TryGetBlock(in FieldPosition fieldPosition, out Block block)
        {
            if (ContainsPosition(fieldPosition) == false)
            {
                block = null;
                return false;
            }
            
            block = this[fieldPosition];
            return block != null && block.IsDestroyed == false;
        }

        public void Clear()
        {
            _blocks.Clear();
            Width = 0;
            Height = 0;
            StartActiveBlocksCount = 0;
        }

        public bool ContainsPosition(int row, int col) =>
            row >= 0 && row < Height && col >= 0 && col < Width;

        public bool ContainsPosition(FieldPosition fieldPosition) => ContainsPosition(fieldPosition.Row, fieldPosition.Col);

        public FieldPosition GetBlockPosition(Block block)
        {
            var index = _blocks.IndexOf(block);
            
            if (index == -1)
            {
                return FieldPosition.None;
            }
            
            var row = 0;

            while (index > Width)
            {
                row++;
                index -= Width;
            }

            return new FieldPosition(row, index);
        }

        public void RemoveBlock(Block block)
        {
            //_blocks.Remove(block);
            BlockRemoved?.Invoke(block);
        }

        public void AddBlock(Block block)
        {
            _blocks.Add(block);
            BlockAdded?.Invoke(block);
        }
        
        public int ActiveBlocksCount => _blocks.Count(x => x != null && x.IsActive && x.IsDestroyed == false);

        private int CalculateIndex(int row, int col) => row * Width + col;
    }
}