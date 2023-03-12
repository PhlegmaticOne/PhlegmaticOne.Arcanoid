using System.Collections.Generic;
using UnityEngine;

namespace Game.GameEntities.Blocks.Behaviors.Common.BallDamage
{
    [CreateAssetMenu(menuName = "Game/Blocks/Behaviors/Create block cracks configuration")]
    public class BlockCracksConfiguration : ScriptableObject
    {
        [SerializeField] private List<Sprite> _crackSprites;
        
        public List<Sprite> CrackSprites => _crackSprites;
    }
}