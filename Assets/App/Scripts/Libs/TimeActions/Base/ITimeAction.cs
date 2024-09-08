namespace Libs.TimeActions.Base
{
    public interface ITimeAction
    {
        float ExecutionTime { get; }
        float RemainTime { get; set; }
        void OnStart();
        void OnUpdate(float deltaTime);
        void OnEnd();
    }
}