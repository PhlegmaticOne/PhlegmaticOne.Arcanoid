using Libs.Behaviors;
using Libs.Pooling.Base;
using UnityEngine;

namespace Game.GameEntities.PlayerObjects.BallObject.Behaviors.Bottom
{
    public class ReturnBallToPoolBehavior : IObjectBehavior<Ball>
    {
        private readonly IPoolProvider _poolProvider;

        public ReturnBallToPoolBehavior(IPoolProvider poolProvider) => _poolProvider = poolProvider;

        public void Behave(Ball entity, Collision2D collision2D)
        {
            var pool = _poolProvider.GetPool<Ball>();
            pool.ReturnToPool(entity);
        }
    }
}