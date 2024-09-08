using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.GameEntities.Blocks.Configurations
{
    [CreateAssetMenu(menuName = "Game/Blocks/Create block configuration", order = 0)]
    public class BlockConfiguration : ScriptableObject
    {
        [SerializeField] private int _blockId;
        [SerializeField] private int _lifesCount;
        [SerializeField] private Sprite _mainSprite;
        [SerializeField] private bool _isActive = true;
        [SerializeField] private bool _hasUnderlyingConfiguration;
        
        [SerializeField]
        [ShowIf(nameof(_hasUnderlyingConfiguration))] 
        private UnderlyingBlockConfiguration _underlyingBlockConfiguration;

        [SerializeField] [ShowIf(nameof(_underlyingBlockConfiguration))]
        private bool _isColorBlock;
        
        
        public int BlockId => _blockId;
        public bool HasUnderlyingConfiguration => _hasUnderlyingConfiguration;
        public int LifesCount => _lifesCount;
        public bool IsColorBlock => _isColorBlock;
        public Sprite MainSprite => _mainSprite;
        public UnderlyingBlockConfiguration UnderlyingBlockConfiguration => _underlyingBlockConfiguration;
        public bool IsActive => _isActive;
    }
}