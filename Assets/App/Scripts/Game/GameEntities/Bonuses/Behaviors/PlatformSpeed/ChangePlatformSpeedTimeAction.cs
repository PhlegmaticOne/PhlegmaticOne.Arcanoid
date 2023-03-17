using Game.GameEntities.PlayerObjects.ShipObject;
using Libs.TimeActions.Base;

namespace Game.GameEntities.Bonuses.Behaviors.PlatformSpeed
{
    public class ChangePlatformSpeedTimeAction : TimeActionBase
    {
        private readonly Ship _ship;
        private readonly float _changeBy;

        public ChangePlatformSpeedTimeAction(Ship ship, float changeBy, bool isIncrease, float executionTime) :
            base(executionTime)
        {
            IsIncrease = isIncrease;
            _ship = ship;
            _changeBy = changeBy;
        }
        public bool IsIncrease { get; }

        public override void OnStart()
        {
            if (IsIncrease)
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
            if (IsIncrease)
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