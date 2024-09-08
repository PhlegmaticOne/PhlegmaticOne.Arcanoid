using UnityEngine;

namespace Game.GameEntities.Blocks.Configurations
{
    [CreateAssetMenu(menuName = "Game/Blocks/Create underlying block configuration")]
    public class UnderlyingBlockConfiguration : ScriptableObject
    {
        [SerializeField] private int _id;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private Color _color;

        public int Id => _id;
        public Sprite Sprite => _sprite;
        public Color Color => _color;
    }
}