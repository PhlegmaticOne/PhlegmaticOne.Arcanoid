using UnityEngine;

namespace Game.GameEntities.PlayerObjects.ShipObject
{
    [CreateAssetMenu(menuName = "Game/Ship/Create ship configuration", order = 0)]
    public class ShipConfiguration : ScriptableObject
    {
        [SerializeField] [Range(0f, 1f)] private float _startControlLerp;
        public float StartControlLerp => _startControlLerp;
    }
}