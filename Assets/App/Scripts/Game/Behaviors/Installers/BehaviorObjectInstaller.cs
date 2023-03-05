using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Behaviors.Installer
{
    [Serializable]
    public class BehaviorObjectInstaller<T> where T : BehaviorObject<T>
    {
        [SerializeField] private List<BehaviorInstallerItem<T>> _onCollisionBehaviourInstallers;
        [SerializeField] private List<BehaviorInstallerItem<T>> _onDestroyBehaviourInstallers;

        public void InstallCollisionBehaviours(T item)
        {
            foreach (var behaviourInstallerItem in _onCollisionBehaviourInstallers)
            {
                foreach (var behaviourInstaller in behaviourInstallerItem.BehaviourInstallers)
                {
                    item.AddOnCollisionBehaviour(behaviourInstallerItem.ColliderTag.Tag, behaviourInstaller.CreateBehaviour());
                }
            }
        }
        
        public void InstallDestroyBehaviours(T item)
        {
            foreach (var behaviourInstallerItem in _onDestroyBehaviourInstallers)
            {
                foreach (var behaviourInstaller in behaviourInstallerItem.BehaviourInstallers)
                {
                    item.AddOnDestroyBehaviour(behaviourInstallerItem.ColliderTag.Tag, behaviourInstaller.CreateBehaviour());
                }
            }
        }
    }
}