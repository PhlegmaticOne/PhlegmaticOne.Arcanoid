using Game.GameEntities.PlayerObjects.BallObject;
using Libs.TimeActions.Base;

namespace Game.GameEntities.Bonuses.Behaviors.ChangeBallsSpeed
{
    public class ChangeBallSpeedTimeAction : TimeActionBase
    {
        private readonly BallsOnField _ballsOnField;
        private readonly float _speedToChange;
        private readonly bool _isAdding;

        public ChangeBallSpeedTimeAction(float executionTime, 
            BallsOnField ballsOnField,
            float speedToChange,
            bool isAdding) : base(executionTime)
        {
            _ballsOnField = ballsOnField;
            _speedToChange = speedToChange;
            _isAdding = isAdding;
        }

        public override void OnStart() => ChangeSpeeds(_isAdding ? _speedToChange : -_speedToChange);

        public override void OnUpdate(float deltaTime) { }

        public override void OnEnd() => ChangeSpeeds(_isAdding ? -_speedToChange : _speedToChange);

        private void ChangeSpeeds(float speed)
        {
            foreach (var ball in _ballsOnField.All)
            {
                var ballSpeed = ball.GetSpeed();
                var speedToAdd = ballSpeed.normalized * speed;
                ball.SetSpeed(ballSpeed + speedToAdd);
            }
        }
    }
}