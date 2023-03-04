using Game.Blocks.CollisionBehaviours.Base;
using UnityEngine;

namespace Game.Blocks.CollisionBehaviours.Log
{
    public class LogCollisionBehaviour : ICollisionBehaviour
    {
        public void OnCollision(Block block)
        {
            Debug.Log(block);
        }
    }
}