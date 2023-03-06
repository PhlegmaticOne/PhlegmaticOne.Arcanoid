using Game.Behaviors.Installer;
using Libs.Pooling.Base;

namespace Game.PlayerObjects.BallObject.Spawners
{
    public class BallSpawner : IBallSpawner
    {
        private readonly BehaviorObjectInstaller<Ball> _ballBehaviorInstaller;
        private IObjectPool<Ball> _ballsPool;
        
        public BallSpawner(IPoolProvider poolProvider,
            BehaviorObjectInstaller<Ball> ballBehaviorInstaller)
        {
            _ballBehaviorInstaller = ballBehaviorInstaller;
            _ballsPool = poolProvider.GetPool<Ball>();
        }
        
        
        public Ball CreateBall(BallCreationContext ballCreationContext)
        {
            var ball = _ballsPool.Get();
            ball.transform.position = ballCreationContext.Position;
            ball.Initialize(ballCreationContext.StartSpeed);
            
            _ballBehaviorInstaller.InstallCollisionBehaviours(ball);
            _ballBehaviorInstaller.InstallDestroyBehaviours(ball);
            
            return ball;
        }
    }
}