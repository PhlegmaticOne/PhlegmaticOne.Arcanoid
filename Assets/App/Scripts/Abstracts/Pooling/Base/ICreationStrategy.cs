using System;

namespace Abstracts.Pooling.Base
{
    public interface ICreationStrategy<out T> where T : IPoolable
    {
        Type ObjectType { get; }
        T Create();
    }
}