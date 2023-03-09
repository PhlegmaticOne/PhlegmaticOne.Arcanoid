using System.Collections.Generic;
using Game.Behaviors;
using UnityEngine;

namespace Game.Blocks.Behaviors.Damage
{
    public class BallDamageBehavior : IObjectBehavior<Block>
    {
        private List<Sprite> _crackSprites;
        
        public void SetBehaviourParameters(BlockCracksConfiguration blockCracksConfiguration)
        {
            _crackSprites = blockCracksConfiguration.CrackSprites;
        }
        
        public void Behave(Block entity, Collision2D collision2D)
        {
            var stageFloat = (float)(entity.StartHealth - entity.CurrentHealth) / _crackSprites.Count;
            var stage = (int)Mathf.Ceil(stageFloat);
            var crackSprite = _crackSprites[stage];
            entity.BlockView.AddSprite(crackSprite);
            entity.Damage();
        }
    }
}