using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Popups.MainGame.Views
{
    public class HealthBarView : MonoBehaviour
    {
        [SerializeField] private HealthView _healthView;
        [SerializeField] private GridLayoutGroup _gridLayoutGroup;

        private readonly List<HealthView> _healthViews = new List<HealthView>();
        private int _currentActiveHealthIndex;

        public void Initialize(int lifesCount)
        {
            _currentActiveHealthIndex = 0;
            for (var i = 0; i < lifesCount; i++)
            {
                var healthView = Instantiate(_healthView, _gridLayoutGroup.transform);
                healthView.Activate();
                _healthViews.Add(healthView);
            }
        }

        public void LoseHealth()
        {
            if (_currentActiveHealthIndex == _healthViews.Count)
            {
                return;    
            }
            
            _healthViews[_currentActiveHealthIndex].Deactivate();
            _currentActiveHealthIndex++;
        }

        public void AddHealth()
        {
            if (_currentActiveHealthIndex == -1)
            {
                return;    
            }
            
            _healthViews[_currentActiveHealthIndex].Activate();
            _currentActiveHealthIndex--;
        }

        public void Clear()
        {
            foreach (var healthView in _healthViews)
            {
                Destroy(healthView.gameObject);
            }
            _healthViews.Clear();
        }
    }
}