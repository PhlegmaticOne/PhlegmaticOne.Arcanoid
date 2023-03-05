using Game.Behaviors;
using Game.Behaviors.Installer;

namespace Game.Blocks.Behaviors.Log
{
    public class LogBehaviorInstaller : BehaviorInstaller<Block>
    {
        public override IObjectBehavior<Block> CreateBehaviour()
        {
            return new LogBehaviour();
        }
    }
}