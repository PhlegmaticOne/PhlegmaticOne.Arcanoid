using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.GameEntities.PlayerObjects.BallObject
{
    public class BallsOnField : MonoBehaviour
    {
        private readonly List<Ball> _balls = new List<Ball>();
        
        public IReadOnlyList<Ball> All => _balls;
        public event UnityAction<Ball> BallAdded; 
        public event UnityAction<Ball> BallRemoved; 

        public void AddBall(Ball ball)
        {
            _balls.Add(ball);
            BallAdded?.Invoke(ball);
        }

        public void RemoveBall(Ball ball)
        {
            _balls.Remove(ball);
            BallRemoved?.Invoke(ball);
        }

        public void Clear() => _balls.Clear();
    }
}