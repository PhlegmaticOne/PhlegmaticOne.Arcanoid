using System;
using System.Collections.Generic;
using UnityEngine;

namespace Libs.Behaviors.Installer
{
    [Serializable]
    public class BehaviorObjectInstaller<T> where T : BehaviorObject<T>
    {
        [SerializeField] private List<BehaviorInstallerItem<T>> _onCollisionBehaviourInstallers;
        [SerializeField] private List<BehaviorInstallerItem<T>> _onDestroyBehaviourInstallers;

        public void InstallCollisionBehaviours(T item)
        {
            var onCollisionBehaviors = item.OnCollisionBehaviors;
            
            foreach (var behaviourInstallerItem in _onCollisionBehaviourInstallers)
            {
                foreach (var behaviourInstaller in behaviourInstallerItem.BehaviourInstallers)
                {
                    onCollisionBehaviors
                        .AddBehavior(behaviourInstallerItem.ColliderTag.Tag, behaviourInstaller.CreateBehaviour());
                }
            }
        }
        
        public void InstallDestroyBehaviours(T item)
        {
            var onDestroyBehaviors = item.OnDestroyBehaviors;
            
            foreach (var behaviourInstallerItem in _onDestroyBehaviourInstallers)
            {
                foreach (var behaviourInstaller in behaviourInstallerItem.BehaviourInstallers)
                {
                    onDestroyBehaviors
                        .AddBehavior(behaviourInstallerItem.ColliderTag.Tag, behaviourInstaller.CreateBehaviour());
                }
            }
        }
    }
}