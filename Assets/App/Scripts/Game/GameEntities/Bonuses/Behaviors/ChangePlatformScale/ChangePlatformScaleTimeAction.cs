using Game.GameEntities.PlayerObjects.ShipObject;
using Libs.TimeActions.Base;

namespace Game.GameEntities.Bonuses.Behaviors.ChangePlatformScale
{
    public class ChangePlatformScaleTimeAction : TimeActionBase
    {
        private readonly Ship _ship;
        private readonly float _scaleBy;
        private readonly float _changingTime;
        private readonly bool _isIncrease;

        public ChangePlatformScaleTimeAction(Ship ship, float scaleBy, float changingTime, 
            bool isIncrease, float executionTime) : 
            base(executionTime)
        {
            _ship = ship;
            _scaleBy = scaleBy;
            _changingTime = changingTime;
            _isIncrease = isIncrease;
        }

        public override void OnStart()
        {
            if (_isIncrease)
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
            if (_isIncrease)
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