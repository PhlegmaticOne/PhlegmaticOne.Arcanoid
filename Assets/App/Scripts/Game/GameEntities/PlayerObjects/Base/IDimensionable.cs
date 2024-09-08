﻿using UnityEngine;

namespace Game.GameEntities.PlayerObjects.Base
{
    public interface IDimensionable
    {
        Transform GetTransform();
        Collider2D GetCollider();
    }
}