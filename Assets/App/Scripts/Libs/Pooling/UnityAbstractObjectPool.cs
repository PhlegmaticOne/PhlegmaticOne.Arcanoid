using System.Collections.Generic;
using System.Linq;
using Libs.Pooling.Base;
using Libs.Pooling.Implementation;
using UnityEngine;

namespace Libs.Pooling
{
    public class UnityAbstractObjectPool<T> : AbstractObjectPool<T> where T : MonoBehaviour, IPoolable
    {
        public UnityAbstractObjectPool(IEnumerable<PrefabInfo<T>> prefabs, Transform poolTransform = null) : base(
            prefabs.Select(x => new MonoFuncCreationStrategy<T>(x)), new MonoPoolableBehaviour<T>(poolTransform)) { }
    }
}