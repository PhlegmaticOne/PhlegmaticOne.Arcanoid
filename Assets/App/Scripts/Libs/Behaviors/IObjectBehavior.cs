﻿using UnityEngine;

namespace Libs.Behaviors
{
    public interface IObjectBehavior { }
    
    public interface IObjectBehavior<in T> : IObjectBehavior where T : BehaviorObject<T>
    {
        public bool IsDefault { get; }
        void Behave(T entity, Collision2D collision2D);
    }
}