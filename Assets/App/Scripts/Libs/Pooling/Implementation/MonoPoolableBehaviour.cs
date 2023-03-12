using Libs.Pooling.Base;
using UnityEngine;

namespace Libs.Pooling.Implementation
{
    public class MonoPoolableBehaviour<T> : IPoolableBehaviour<T> where T : MonoBehaviour, IPoolable
    {
        private readonly Transform _poolTransform;

        public MonoPoolableBehaviour(Transform poolTransform)
        {
            _poolTransform = poolTransform;
        }
        public void Enable(T item) => item.gameObject.SetActive(true);
        public void Disable(T item)
        {
            item.transform.SetParent(_poolTransform);
            item.gameObject.SetActive(false);
        }

        public void Destroy(T item) => Object.Destroy(item.gameObject);
    }
}