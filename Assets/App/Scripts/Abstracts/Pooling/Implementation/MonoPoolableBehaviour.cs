using Abstracts.Pooling.Base;
using UnityEngine;

namespace Abstracts.Pooling.Implementation
{
    public class MonoPoolableBehaviour<T> : IPoolableBehaviour<T> where T : MonoBehaviour, IPoolable
    {
        public void Enable(T item) => item.gameObject.SetActive(true);
        public void Disable(T item) => item.gameObject.SetActive(false);
        public void Destroy(T item) => Object.Destroy(item.gameObject);
    }
}