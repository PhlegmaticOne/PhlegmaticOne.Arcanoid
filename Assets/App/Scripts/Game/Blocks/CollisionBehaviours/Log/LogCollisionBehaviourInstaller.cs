using Game.Blocks.CollisionBehaviours.Base;

namespace Game.Blocks.CollisionBehaviours.Log
{
    public class LogCollisionBehaviourInstaller : CollisionBehaviourInstaller
    {
        public override ICollisionBehaviour CreateCollisionBehaviour()
        {
            return new LogCollisionBehaviour();
        }
    }
}