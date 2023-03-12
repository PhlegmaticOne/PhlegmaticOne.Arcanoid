using Game.PlayerObjects.BallObject;
using Libs.TimeActions.Base;

namespace Game.Bonuses.Behaviors.IncreaseBallsSpeed
{
    public class IncreaseBallSpeedTimeAction : TimeActionBase
    {
        private readonly BallsOnField _ballsOnField;
        private readonly float _speedToAdd;

        public IncreaseBallSpeedTimeAction(float executionTime, 
            BallsOnField ballsOnField,
            float speedToAdd) : base(executionTime)
        {
            _ballsOnField = ballsOnField;
            _speedToAdd = speedToAdd;
        }

        public override void OnStart() => IncreaseSpeeds(_speedToAdd);

        public override void OnUpdate(float deltaTime) { }

        public override void OnEnd() => IncreaseSpeeds(-_speedToAdd);

        private void IncreaseSpeeds(float speed)
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