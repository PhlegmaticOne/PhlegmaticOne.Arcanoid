using UnityEngine;

namespace Game.GameEntities.Bullets.Spawner
{
    public interface IBulletSpawner
    {
        Bullet CreateBullet(BulletCreationContext bulletCreationContext);
    }

    public class BulletCreationContext
    {
        public Vector2 Position { get; set; }
    }
}