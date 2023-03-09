using System.Collections.Generic;
using System.Linq;
using Game.Blocks;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Field
{
    public class GameField
    {
        private readonly List<Block> _blocks;
        public event UnityAction<Block> BlockAdded;
        public event UnityAction<Block> BlockRemoved; 
        public int Width { get; }
        public int Height { get; }
        public Bounds Bounds { get; }
        public IReadOnlyList<Block> Blocks => _blocks;

        public GameField(int width, int height, Bounds bounds, IEnumerable<Block> blocks)
        {
            _blocks = blocks.ToList();
            Width = width;
            Height = height;
            Bounds = bounds;
        }

        public Block this[int row, int col] => _blocks[row * Width + col];

        public void RemoveBlock(Block block)
        {
            _blocks.Remove(block);
            BlockAdded?.Invoke(block);
        }

        public void AddBlock(Block block)
        {
            _blocks.Add(block);
            BlockRemoved?.Invoke(block);
        }

        public int ActiveBlocksCount => _blocks.Count(x => x.BlockConfiguration.ActiveOnPlay);
        public int NotDestroyedBlocksCount => _blocks.Count(x => x.IsDestroyed == false);
    }
}