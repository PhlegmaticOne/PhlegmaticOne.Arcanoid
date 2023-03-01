using Abstracts.Pooling.Base;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Abstracts.Pooling.Implementation
{
    public class MonoFuncCreationStrategy<T> : FuncCreationStrategy<T> where T : MonoBehaviour, IPoolable
    {
        public MonoFuncCreationStrategy(PrefabInfo<T> prefabInfo) :
            base(prefabInfo.Prefab.GetType(), () => Object.Instantiate(prefabInfo.Prefab, prefabInfo.Parent)) { }
    }
}