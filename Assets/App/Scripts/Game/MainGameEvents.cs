using System;
using Game.Base;

namespace Game
{
    public class MainGameEvents : IGameEvents
    {
        public event Action HealthLost;
        public event Action HealthAdded;
        public event Action<BlockDestroyedEventArgs> BlockDestroyed;
        public void OnLoseHealth() => HealthLost?.Invoke();
        public void OnAddHealth() => HealthAdded?.Invoke();
        public void OnBlockDestroyed(BlockDestroyedEventArgs blockDestroyedEvents) => BlockDestroyed?.Invoke(blockDestroyedEvents);
    }

    public class BlockDestroyedEventArgs
    {
        public int ActiveBlocksCount { get; set; }
        public int RemainBlocksCount { get; set; }
    }
}