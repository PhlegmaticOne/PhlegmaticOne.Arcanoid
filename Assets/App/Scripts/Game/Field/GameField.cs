using System.Collections.Generic;
using System.Linq;
using Game.Blocks;
using UnityEngine;

namespace Game.Field
{
    public class GameField : MonoBehaviour
    {
        private readonly List<Block> _blocks = new List<Block>();
        public int Width { get; private set; }
        public int Height { get; private set; }

        public void Initialize(IEnumerable<Block> blocks, int width, int height)
        {
            _blocks.Clear();
            _blocks.AddRange(blocks);
            Width = width;
            Height = height;
        }

        public void RemoveBlock(Block block) => _blocks.Remove(block);

        public void AddBlock(Block block) => _blocks.Add(block);

        public int ActiveBlocksCount => _blocks.Count(x => x.BlockConfiguration.ActiveOnPlay);
    }
}