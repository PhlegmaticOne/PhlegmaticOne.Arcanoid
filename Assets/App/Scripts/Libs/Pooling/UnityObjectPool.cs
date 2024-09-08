using Libs.Pooling.Base;
using Libs.Pooling.Implementation;
using UnityEngine;

namespace Libs.Pooling
{
    public class UnityObjectPool<T> : ObjectPool<T> where T : MonoBehaviour, IPoolable
    {
        public UnityObjectPool(PrefabInfo<T> prefabInfo, int initialCapacity, int maxCapacity,
            bool destroyItemsOnOverflow = true, Transform poolTransform = null) :
            base(new MonoFuncCreationStrategy<T>(prefabInfo), new MonoPoolableBehaviour<T>(poolTransform),
                initialCapacity, maxCapacity, destroyItemsOnOverflow) { }
    }
}