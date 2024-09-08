using Game.GameEntities.PlayerObjects.BallObject;
using Libs.TimeActions.Base;

namespace Game.GameEntities.Bonuses.Behaviors.ChangeBallsSpeed
{
    public class ChangeBallSpeedTimeAction : EndsOfIntervalTimeAction
    {
        private readonly BallsOnField _ballsOnField;
        private readonly float _speedToChange;

        public ChangeBallSpeedTimeAction(float executionTime, 
            BallsOnField ballsOnField,
            float speedToChange,
            bool isAdding) : base(executionTime)
        {
            IsAdding = isAdding;
            _ballsOnField = ballsOnField;
            _speedToChange = speedToChange;
        }
        
        public bool IsAdding { get; }

        public override void OnStart() => ChangeSpeeds(IsAdding ? _speedToChange : -_speedToChange);
        
        public override void OnEnd() => ChangeSpeeds(IsAdding ? -_speedToChange : _speedToChange);

        private void ChangeSpeeds(float speed)
        {
            foreach (var ball in _ballsOnField.All)
            {
                ball.AddDeltaSpeed(speed);
            }
        }
    }
}