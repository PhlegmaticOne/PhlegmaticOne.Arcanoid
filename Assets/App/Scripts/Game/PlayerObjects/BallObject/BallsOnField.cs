using System.Collections.Generic;

namespace Game.PlayerObjects.BallObject
{
    public class BallsOnField
    {
        private readonly List<Ball> _balls = new List<Ball>();
        
        public IReadOnlyList<Ball> All => _balls;

        public void AddBall(Ball ball) => _balls.Add(ball);

        public void RemoveBall(Ball ball) => _balls.Remove(ball);
        public void Clear() => _balls.Clear();
    }
}