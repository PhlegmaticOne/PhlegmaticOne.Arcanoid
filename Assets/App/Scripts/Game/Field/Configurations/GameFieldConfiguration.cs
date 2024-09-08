using System;
using UnityEngine;

namespace Game.Field.Configurations
{
    [CreateAssetMenu(menuName = "Game/Field/Create field configuration", order = 0)]
    public class GameFieldConfiguration : ScriptableObject
    {
        [SerializeField] private MarginInPercentage _fieldMargin;
        [SerializeField] [Range(0f, 100f)] private float _interactableTopLineMargin;
        [SerializeField] private float _blockMarginTop;
        [SerializeField] private float _blockMarginRight;
        
        public float BlockMarginTop => MarginInPercentage.Normalize(_blockMarginTop);
        public float BlockMarginRight => MarginInPercentage.Normalize(_blockMarginRight);
        public float InteractableTopLineMargin => MarginInPercentage.Normalize(_interactableTopLineMargin);
        public MarginInPercentage FieldMargin => _fieldMargin;
    }

    [Serializable]
    public class MarginInPercentage
    {
        [SerializeField] [Range(0f, 100f)] private float _fromTop;
        [SerializeField] [Range(0f, 100f)] private float _fromBottom;
        [SerializeField] [Range(0f, 100f)] private float _fromLeft;
        [SerializeField] [Range(0f, 100f)] private float _fromRight;

        public float FromTop => Normalize(_fromTop);
        public float FromBottom => Normalize(_fromBottom);
        public float FromLeft => Normalize (_fromLeft);
        public float FromRight => Normalize(_fromRight);

        internal static float Normalize(float value) => value / 100f;
    }
}