using System.Collections.Generic;
using UnityEngine;

namespace Game.Blocks.Behaviors.Common.BallDamage
{
    [CreateAssetMenu(menuName = "Game/Blocks/Behaviors/Create block cracks configuration")]
    public class BlockCracksConfiguration : ScriptableObject
    {
        [SerializeField] private List<Sprite> _crackSprites;
        
        public List<Sprite> CrackSprites => _crackSprites;
    }
}