namespace Libs.TimeActions.Base
{
    public abstract class EndsOfIntervalTimeAction : ITimeAction
    {
        protected EndsOfIntervalTimeAction(float executionTime)
        {
            ExecutionTime = executionTime;
            Restart();
        }
        
        public float ExecutionTime { get; }
        public float RemainTime { get; set; }
        
        public abstract void OnStart();
        public abstract void OnEnd();
        
        public void OnUpdate(float deltaTime) { }
        
        public void Restart() => RemainTime = ExecutionTime;
    }
}