using System.Collections.Generic;
using UnityEngine;

namespace Game.Behaviors
{
    public class BehaviorObjectTags : MonoBehaviour
    {
        [SerializeField] private List<ColliderTag> _colliderTags;
        public IReadOnlyList<ColliderTag> ColliderTags => _colliderTags;
    }
}