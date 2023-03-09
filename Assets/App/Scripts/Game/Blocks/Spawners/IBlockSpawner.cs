using Common.Data.Models;
using UnityEngine;

namespace Game.Blocks.Spawners
{
    public interface IBlockSpawner
    {
        Block SpawnBlock(int blockId, BlockSpawnData blockSpawnData);
    }

    public class BlockSpawnData
    {
        public BlockSpawnData(Vector3 size, Vector3 position)
        {
            Size = size;
            Position = position;
        }

        public Vector3 Position { get; }
        public Vector3 Size { get; }
    }
}