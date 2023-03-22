using UnityEngine;
using UnityEngine.UI;

namespace Popups.MainGame.Views
{
    public class ColorBindableComponent : MonoBehaviour
    {
        [SerializeField] private Image _image;

        public Image Image => _image;

        public void BindColor(Color color)
        {
            _image.color = color;
        }
    }
}