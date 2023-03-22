using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Logic.Systems.Health
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private Image _healthImage;
        [SerializeField] private Color _activeColor;
        [SerializeField] private Color _disabledColor;

        [SerializeField] private float _animationDuration;
        
        public void Activate()
        {
            _healthImage.color = _disabledColor;
            ToColor(_activeColor);
        }

        public void Deactivate() => ToColor(_disabledColor);

        private void ToColor(in Color color) => _healthImage.DOColor(color, _animationDuration).SetUpdate(true);
    }
}