namespace Libs.TimeActions.Base
{
    public abstract class TimeActionBase : ITimeAction
    {
        protected TimeActionBase(float executionTime)
        {
            ExecutionTime = executionTime;
            RemainTime = executionTime;
        }
        
        public float ExecutionTime { get; }
        public float RemainTime { get; set; }
        public abstract void OnStart();
        public abstract void OnUpdate(float deltaTime);
        public abstract void OnEnd();
        public void Restart() => RemainTime = ExecutionTime;
    }
}