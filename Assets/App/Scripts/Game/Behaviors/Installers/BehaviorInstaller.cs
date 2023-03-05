using UnityEngine;

namespace Game.Behaviors.Installer
{
    public abstract class BehaviorInstaller<T> : MonoBehaviour where T : BehaviorObject<T>
    {
        public abstract IObjectBehavior<T> CreateBehaviour();
    }
}