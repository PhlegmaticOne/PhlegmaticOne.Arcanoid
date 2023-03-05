using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Behaviors.Installer
{
    [Serializable]
    public class BehaviorInstallerItem<T> where T : BehaviorObject<T>
    {
        [SerializeField] private ColliderTag _colliderTag;
        [SerializeField] private List<BehaviorInstaller<T>> _behaviourInstallers;
        
        public ColliderTag ColliderTag => _colliderTag;
        public List<BehaviorInstaller<T>> BehaviourInstallers => _behaviourInstallers;
    }
}