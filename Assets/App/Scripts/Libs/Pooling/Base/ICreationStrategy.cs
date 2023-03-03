using System;

namespace Libs.Pooling.Base
{
    public interface ICreationStrategy<out T> where T : IPoolable
    {
        Type ObjectType { get; }
        T Create();
    }
}