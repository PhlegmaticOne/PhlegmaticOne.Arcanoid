using System.Collections.Generic;
using UnityEngine;

namespace Libs.Behaviors
{
    public class BehaviorObjectTags : MonoBehaviour
    {
        [SerializeField] private List<ColliderTag> _colliderTags;
        public IReadOnlyList<ColliderTag> ColliderTags => _colliderTags;
        public ColliderTag this[int index] => _colliderTags[index];
    }
}