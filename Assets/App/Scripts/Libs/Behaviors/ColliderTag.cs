﻿using UnityEngine;

namespace Libs.Behaviors
{
    [CreateAssetMenu(menuName = "Behaviours/Create collider tag")]
    public class ColliderTag : ScriptableObject
    {
        [SerializeField] private string _tag;
        public string Tag => _tag;
    }
}