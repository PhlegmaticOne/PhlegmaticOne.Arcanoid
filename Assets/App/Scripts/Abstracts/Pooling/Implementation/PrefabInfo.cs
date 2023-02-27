using Abstracts.Pooling.Base;
using UnityEngine;

namespace Abstracts.Pooling.Implementation
{
    public class PrefabInfo<T> where T : MonoBehaviour, IPoolable
    {
        public T Prefab { get; }
        public Transform Parent { get; }

        public static implicit operator PrefabInfo<T>(T prefab) => new PrefabInfo<T>(prefab, null);

        public PrefabInfo(T prefab, Transform parent)
        {
            Prefab = prefab;
            Parent = parent;
        }
    }
}