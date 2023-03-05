﻿using UnityEngine;

namespace Game.Behaviors
{
    public interface IObjectBehavior<in T> where T : BehaviorObject<T>
    {
        void Behave(T entity, Collision2D collision2D);
    }
}