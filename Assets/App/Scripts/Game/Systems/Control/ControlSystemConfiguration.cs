using UnityEngine;

namespace Game.Systems.Control
{
    [CreateAssetMenu(menuName = "Game/Systems/Create control system configuration", order = 0)]
    public class ControlSystemConfiguration : ScriptableObject
    {
        [SerializeField] [Range(0f, 1f)] private float _lerp;
        public float Lerp => _lerp;
    }
}