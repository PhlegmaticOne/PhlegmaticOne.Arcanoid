using Game.GameEntities.Base;
using UnityEngine.Events;

namespace Game.GameEntities.PlayerObjects.BallObject
{
    public class BallsOnField : EntitiesOnField<Ball>
    {
        protected override void OnAdded(Ball behaviorObject)
        {
            if (MainBall == null)
            {
                MainBall = behaviorObject;
            }
        }

        protected override void OnRemoved(Ball behaviorObject)
        {
            MainBall = _all.Count switch
            {
                1 => _all[0],
                0 => null,
                _ => MainBall
            };

            BallRemoved?.Invoke(behaviorObject);
        }

        public Ball MainBall { get; private set; }
        public event UnityAction<Ball> BallRemoved;
    }
}