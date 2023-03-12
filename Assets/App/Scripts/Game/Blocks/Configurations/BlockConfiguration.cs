﻿using System.Collections.Generic;
using UnityEngine;

namespace Game.Blocks.Configurations
{
    [CreateAssetMenu(menuName = "Game/Blocks/Create block configuration", order = 0)]
    public class BlockConfiguration : ScriptableObject
    {
        [SerializeField] private int _blockId;
        [SerializeField] private int _lifesCount;
        [SerializeField] private bool _activeOnPlay = true;
        [SerializeField] private bool _isExtraBlock;
        [SerializeField] private Sprite _blockSprite;
        [SerializeField] private List<Sprite> _additionalSprites;
        
        public int BlockId => _blockId;
        public bool IsExtraBlock => _isExtraBlock;
        public int LifesCount => _lifesCount;
        public List<Sprite> AdditionalSprites => _additionalSprites ?? new List<Sprite>();
        public Sprite BlockSprite => _blockSprite;
        public bool ActiveOnPlay => _activeOnPlay;
    }
}