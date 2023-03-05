using UnityEngine;

namespace Game.PlayerObjects.BallObject.Factories
{
    public interface IBallSpawner
    {
        Ball CreateBall(BallCreationContext ballCreationContext);
    }

    public class BallCreationContext
    {
        public BallCreationContext(Vector2 position, float startSpeed)
        {
            Position = position;
            StartSpeed = startSpeed;
        }

        public Vector2 Position { get; }
        public float StartSpeed { get; }
    }
}