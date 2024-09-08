using Libs.Behaviors.Installer;
using Libs.Pooling.Base;
using UnityEngine;

namespace Game.GameEntities.Bullets.Spawner
{
    public class BulletSpawner : IBulletSpawner
    {
        private readonly BehaviorObjectInstaller<Bullet> _bulletObjectInstaller;
        private readonly Transform _spawnTransform;
        private readonly IObjectPool<Bullet> _bulletsPool;
        
        public BulletSpawner(IPoolProvider poolProvider,
            BehaviorObjectInstaller<Bullet> bulletObjectInstaller,
            Transform spawnTransform)
        {
            _bulletObjectInstaller = bulletObjectInstaller;
            _spawnTransform = spawnTransform;
            _bulletsPool = poolProvider.GetPool<Bullet>();
        }
        
        public Bullet CreateBullet(BulletCreationContext bulletCreationContext)
        {
            var bullet = _bulletsPool.Get();
            bullet.transform.SetParent(_spawnTransform);
            bullet.transform.position = bulletCreationContext.Position;
            _bulletObjectInstaller.InstallCollisionBehaviours(bullet);
            _bulletObjectInstaller.InstallDestroyBehaviours(bullet);
            return bullet;
        }
    }
}