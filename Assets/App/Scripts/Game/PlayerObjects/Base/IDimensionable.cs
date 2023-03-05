using UnityEngine;

namespace Game.PlayerObjects.Base
{
    public interface IDimensionable
    {
        Transform GetTransform();
        Bounds GetBounds();
    }
}