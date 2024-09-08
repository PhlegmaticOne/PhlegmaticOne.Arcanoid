﻿using System;
using Libs.Pooling.Base;

namespace Libs.Pooling.Implementation
{
    public class FuncCreationStrategy<T> : ICreationStrategy<T> where T : IPoolable
    {
        private readonly Func<T> _factoryFunc;

        public FuncCreationStrategy(Type objectType, Func<T> factoryFunc)
        {
            ObjectType = objectType;
            _factoryFunc = factoryFunc;
        }

        public Type ObjectType { get; }
        public T Create() => _factoryFunc.Invoke();
    }
}