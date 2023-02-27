using System;
using Abstracts.Pooling.Base;

namespace Abstracts.Pooling.Implementation
{
    public class FuncCreationStrategy<T> : ICreationStrategy<T> where T : IPoolable
    {
        private readonly Func<T> _factoryFunc;

        public FuncCreationStrategy(Func<T> factoryFunc) => _factoryFunc = factoryFunc;
        
        public T Create() => _factoryFunc.Invoke();
    }
}