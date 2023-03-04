using UnityEngine;

namespace Game.Blocks.CollisionBehaviours.Base
{
    public abstract class CollisionBehaviourInstaller : MonoBehaviour
    {
        public abstract ICollisionBehaviour CreateCollisionBehaviour();
    }
}