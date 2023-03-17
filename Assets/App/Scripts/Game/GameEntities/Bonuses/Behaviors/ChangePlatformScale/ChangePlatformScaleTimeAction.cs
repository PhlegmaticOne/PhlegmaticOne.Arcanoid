using Game.GameEntities.PlayerObjects.ShipObject;
using Libs.TimeActions.Base;

namespace Game.GameEntities.Bonuses.Behaviors.ChangePlatformScale
{
    public class ChangePlatformScaleTimeAction : TimeActionBase
    {
        public bool IsIncrease { get; }
        private readonly Ship _ship;
        private readonly float _scaleBy;
        private readonly float _changingTime;

        public ChangePlatformScaleTimeAction(Ship ship, float scaleBy, float changingTime, 
            bool isIncrease, float executionTime) : 
            base(executionTime)
        {
            IsIncrease = isIncrease;
            _ship = ship;
            _scaleBy = scaleBy;
            _changingTime = changingTime;
        }

        public override void OnStart()
        {
            if (IsIncrease)
            {
                _ship.IncreaseScaleBy(_scaleBy, _changingTime);
            }
            else
            {
                _ship.DecreaseScaleBy(_scaleBy, _changingTime);
            }
        }

        public override void OnUpdate(float deltaTime) { }

        public override void OnEnd()
        {
            if (IsIncrease)
            {
                _ship.DecreaseScaleBy(_scaleBy, _changingTime);
            }
            else
            {
                _ship.IncreaseScaleBy(_scaleBy, _changingTime);
            }
        }
    }
}