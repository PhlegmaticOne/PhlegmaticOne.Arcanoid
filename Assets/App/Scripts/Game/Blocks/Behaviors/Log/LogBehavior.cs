using Game.Behaviors;
using UnityEngine;

namespace Game.Blocks.Behaviors.Log
{
    public class LogBehaviour : IObjectBehavior<Block>
    {
        public void Behave(Block entity, Collision2D collision2D)
        {
            Debug.Log(entity.BlockConfiguration.BlockId);
        }
    }
}