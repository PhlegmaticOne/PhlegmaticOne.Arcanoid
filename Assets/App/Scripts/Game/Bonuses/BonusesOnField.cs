using System.Collections.Generic;
using UnityEngine;

namespace Game.Bonuses
{
    public class BonusesOnField : MonoBehaviour
    {
        private readonly List<Bonus> _bonuses = new List<Bonus>();
        
        public IReadOnlyList<Bonus> All => _bonuses;

        public void AddBonus(Bonus ball) => _bonuses.Add(ball);

        public void RemoveBonus(Bonus ball) => _bonuses.Remove(ball);
        
        public void Clear() => _bonuses.Clear();
    }
}