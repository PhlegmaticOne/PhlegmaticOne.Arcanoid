using System.Collections.Generic;
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

        public Block this[int row, int col] => _blocks[row * Width + col];

        public void RemoveBlock(Block block)
        {
            _blocks.Remove(block);
            BlockRemoved?.Invoke(block);
        }

        public void AddBlock(Block block)
        {
            _blocks.Add(block);
            BlockAdded?.Invoke(block);
        }
        
        public int ActiveBlocksCount => _blocks.Count(x => x.BlockConfiguration.ActiveOnPlay);
    }
}