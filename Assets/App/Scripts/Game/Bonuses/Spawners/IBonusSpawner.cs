using Game.Bonuses.Configurations;
using UnityEngine;

namespace Game.Bonuses.Spawners
{
    public interface IBonusSpawner
    {
        Bonus SpawnBonus(BonusConfiguration bonusConfiguration, BonusSpawnData bonusSpawnData);
    }

    public class BonusSpawnData
    {
        public Vector2 Position { get; set; }
    }
}