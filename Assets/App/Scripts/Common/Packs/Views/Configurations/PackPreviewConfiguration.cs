using UnityEngine;

namespace Common.Packs.Views.Configurations
{
    [CreateAssetMenu(menuName = "Packs/Views/Create pack preview configuration")]
    public class PackPreviewConfiguration : ScriptableObject
    {
        [SerializeField] private Color _innerColor;
        [SerializeField] private Color _outerColor;
        [SerializeField] private Color _glowColor;
        [SerializeField] private Color _levelTextColor;
        [SerializeField] private Color _mainTextColor;
        public Color InnerColor => _innerColor;
        public Color OuterColor => _outerColor;
        public Color GlowColor => _glowColor;
        public Color LevelTextColor => _levelTextColor;
        public Color MainTextColor => _mainTextColor;
    }
}