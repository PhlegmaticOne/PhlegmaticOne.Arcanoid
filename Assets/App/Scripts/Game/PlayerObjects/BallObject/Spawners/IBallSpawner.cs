using UnityEngine;

namespace Game.PlayerObjects.BallObject.Spawners
{
    public interface IBallSpawner
    {
        Ball CreateBall(BallCreationContext ballCreationContext);
    }

    public class BallCreationContext
    {
        public Vector2 Position { get; set; }
        public float StartSpeed { get; set; }
        public bool SetSpecifiedStartSpeed { get; set; }
    }
}