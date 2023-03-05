using UnityEngine;

namespace Game.Blocks.Configurations
{
    [CreateAssetMenu(menuName = "Game/Blocks/Create block configuration", order = 0)]
    public class BlockConfiguration : ScriptableObject
    {
        [SerializeField] private int _blockId;
        [SerializeField] private Sprite _blockSprite;
        [SerializeField] private bool _activeOnPlay = true;
        
        public int BlockId => _blockId;
        public Sprite BlockSprite => _blockSprite;
        public bool ActiveOnPlay => _activeOnPlay;
    }
}