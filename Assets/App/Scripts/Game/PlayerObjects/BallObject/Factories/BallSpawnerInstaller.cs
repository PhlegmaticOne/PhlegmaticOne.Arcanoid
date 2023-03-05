using Game.Behaviors.Installer;
using Libs.Pooling;
using Libs.Pooling.Base;
using UnityEngine;

namespace Game.PlayerObjects.BallObject.Factories
{
    public class BallSpawnerInstaller : MonoBehaviour
    {
        [SerializeField] private BehaviorObjectInstaller<Ball> _ballBehaviorsInstaller;
        [SerializeField] private UnityObjectPoolInstaller<Ball> _ballPoolInstaller;

        public UnityObjectPoolInstaller<Ball> BallPoolInstaller => _ballPoolInstaller;

        public IBallSpawner CreateBallSpawner(IPoolProvider poolProvider)
        {
            return new BallSpawner(poolProvider, _ballBehaviorsInstaller);
        }
    }
}