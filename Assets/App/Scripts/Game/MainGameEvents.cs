using System;
using Game.Base;

namespace Game
{
    public class MainGameEvents : IGameEvents
    {
        public event Action HealthLost;
        public event Action HealthAdded;
        public void LoseHealth() => HealthLost?.Invoke();
        public void AddHealth() => HealthAdded?.Invoke();
    }
}