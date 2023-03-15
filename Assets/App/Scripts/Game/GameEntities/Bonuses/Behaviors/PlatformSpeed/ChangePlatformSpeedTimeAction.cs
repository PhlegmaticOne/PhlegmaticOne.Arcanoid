using Game.GameEntities.PlayerObjects.ShipObject;
using Libs.TimeActions.Base;

namespace Game.GameEntities.Bonuses.Behaviors.PlatformSpeed
{
    public class ChangePlatformSpeedTimeAction : TimeActionBase
    {
        private readonly Ship _ship;
        private readonly float _changeBy;
        private readonly bool _isIncrease;

        public ChangePlatformSpeedTimeAction(Ship ship, float changeBy, bool isIncrease, float executionTime) :
            base(executionTime)
        {
            _ship = ship;
            _changeBy = changeBy;
            _isIncrease = isIncrease;
        }

        public override void OnStart()
        {
            if (_isIncrease)
            {
                _ship.IncreaseLerpBy(_changeBy);
            }
            else
            {
                _ship.DecreaseLerpBy(_changeBy);
            }
        }

        public override void OnUpdate(float deltaTime) { }

        public override void OnEnd()
        {
            if (_isIncrease)
            {
                _ship.DecreaseLerpBy(_changeBy);
            }
            else
            {
                _ship.IncreaseLerpBy(_changeBy);
            }
        }
    }
}