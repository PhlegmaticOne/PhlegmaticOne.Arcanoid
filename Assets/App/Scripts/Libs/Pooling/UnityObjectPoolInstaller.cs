using System;
using Libs.Pooling.Base;
using Libs.Pooling.Implementation;
using UnityEngine;

namespace Libs.Pooling
{
    [Serializable]
    public class UnityObjectPoolInstaller<T> where T : MonoBehaviour, IPoolable
    {
        [SerializeField] private T _prefab;
        [SerializeField] private Transform _spawnTransform;
        [SerializeField] private int _initialCapacity;
        [SerializeField] private int _maxCapacity;
        [SerializeField] private bool _destroyOnOverflow;

        public IObjectPool<T> CreateObjectPool() =>
            new UnityObjectPool<T>(new PrefabInfo<T>(_prefab, _spawnTransform),
                _initialCapacity, _maxCapacity, _destroyOnOverflow);
        
        public IObjectPool<T> CreateObjectPool(Transform spawnTransform) =>
            new UnityObjectPool<T>(new PrefabInfo<T>(_prefab, spawnTransform),
                _initialCapacity, _maxCapacity, _destroyOnOverflow, spawnTransform);
    }
}