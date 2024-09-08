using UnityEngine;

namespace Game.GameEntities.PlayerObjects.Base
{
    public interface IStartMovable : IDimensionable
    {
        void StartMove(Vector2 direction);
    }
}