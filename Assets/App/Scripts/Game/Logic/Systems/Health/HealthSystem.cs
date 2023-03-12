using UnityEngine;
using UnityEngine.Events;

namespace Game.Logic.Systems.Health
{
    public class HealthSystem : MonoBehaviour
    {
        private int _startHealthCount;
        private int _currentHealthCount;
        
        public event UnityAction AllHealthLost;
        public event UnityAction HealthLost;
        public event UnityAction HealthAdded;

        public void Initialize(int startHealthCount)
        {
            _startHealthCount = startHealthCount;
            _currentHealthCount = startHealthCount;
        }

        public void LoseHealth()
        {
            _currentHealthCount--;
            HealthLost?.Invoke();

            if (_currentHealthCount == 0)
            {
                AllHealthLost?.Invoke();
            }
        }

        public void AddHealth()
        {
            if (_currentHealthCount == _startHealthCount)
            {
                return;
            }

            ++_currentHealthCount;
            HealthAdded?.Invoke();
        }
    }
}