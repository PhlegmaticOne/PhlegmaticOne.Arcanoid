namespace Libs.TimeActions.Base
{
    public abstract class FixedIntervalTimeAction : ITimeAction
    {
        private readonly float _fixedInterval;
        private float _currentIntervalTime;
        private int _currentInterval;
        private int _actionsCount;

        protected FixedIntervalTimeAction(float fixedInterval, int actionsCount)
        {
            _actionsCount = actionsCount;
            _fixedInterval = fixedInterval;
            CalculateExecutionTime(actionsCount);
        }

        protected void SetActionsCount(int actionsCount)
        {
            _actionsCount = actionsCount;
            CalculateExecutionTime(actionsCount);
        }

        public float ExecutionTime { get; private set; }
        public float RemainTime { get; set; }
        
        public abstract void OnStart();
        public abstract void OnEnd();
        protected abstract void OnInterval(int interval);

        public void Reset()
        {
            _currentInterval = 0;
            _currentIntervalTime = 0;
            CalculateExecutionTime(_actionsCount);
        }

        public void OnUpdate(float deltaTime)
        {
            if (_currentIntervalTime == 0f)
            {
                RemainTime = (_actionsCount - _currentInterval) * _fixedInterval;
                OnInterval(_currentInterval);
                ++_currentInterval;
            }

            _currentIntervalTime += deltaTime;

            if (_currentIntervalTime >= _fixedInterval)
            {
                _currentIntervalTime = 0f;
            }
        }

        private void CalculateExecutionTime(int actionsCount)
        {
            ExecutionTime = _fixedInterval * actionsCount;
            RemainTime = ExecutionTime;
        }
    }
}