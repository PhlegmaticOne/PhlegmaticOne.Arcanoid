using Game.GameEntities.Bonuses.Behaviors.CaptiveBall;
using Game.Logic.Systems.Control;
using Game.Logic.Systems.Health;

namespace Game.Composites
{
    public class GameSystems
    {
        public HealthSystem HealthSystem { get; }
        public ControlSystem ControlSystem { get; }
        public CaptiveBallsSystem CaptiveBallsSystem { get; }

        public GameSystems(HealthSystem healthSystem,
            ControlSystem controlSystem,
            CaptiveBallsSystem captiveBallsSystem)
        {
            HealthSystem = healthSystem;
            ControlSystem = controlSystem;
            CaptiveBallsSystem = captiveBallsSystem;
        }

        public void Disable()
        {
            ControlSystem.Disable();
            CaptiveBallsSystem.Disable();
        }

        public void Enable()
        {
            ControlSystem.Enable();
        }
    }
}