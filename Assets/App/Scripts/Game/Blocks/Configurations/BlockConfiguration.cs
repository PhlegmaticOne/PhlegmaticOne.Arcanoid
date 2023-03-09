using System.Collections.Generic;
using UnityEngine;

namespace Game.Blocks.Configurations
{
    [CreateAssetMenu(menuName = "Game/Blocks/Create block configuration", order = 0)]
    public class BlockConfiguration : ScriptableObject
    {
        [SerializeField] private int _blockId;
        [SerializeField] private int _dropBlockId;
        [SerializeField] private int _lifesCount;
        [SerializeField] private Sprite _blockSprite;
        [SerializeField] private List<Sprite> _additionalSprites;
        [SerializeField] private bool _gravitable = false;
        [SerializeField] private bool _activeOnPlay = true;
        
        public int BlockId => _blockId;
        public int DropBlockId => _dropBlockId;
        public int LifesCount => _lifesCount;
        public List<Sprite> AdditionalSprites => _additionalSprites ?? new List<Sprite>();
        public bool Gravitable => _gravitable;
        public Sprite BlockSprite => _blockSprite;
        public bool ActiveOnPlay => _activeOnPlay;
    }
}