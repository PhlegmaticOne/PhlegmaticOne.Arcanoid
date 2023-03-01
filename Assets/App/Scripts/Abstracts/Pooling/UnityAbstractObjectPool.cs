using System.Collections.Generic;
using System.Linq;
using Abstracts.Pooling.Base;
using Abstracts.Pooling.Implementation;
using UnityEngine;

namespace Abstracts.Pooling
{
    public class UnityAbstractObjectPool<T> : AbstractObjectPool<T> where T : MonoBehaviour, IPoolable
    {
        public UnityAbstractObjectPool(IEnumerable<PrefabInfo<T>> prefabs) : base(
            prefabs.Select(x => new MonoFuncCreationStrategy<T>(x)), new MonoPoolableBehaviour<T>()) { }
    }
}