using Game.Logic.Systems.Health;
using Game.PopupRequires.Commands.Base;

namespace Game.PopupRequires.Commands
{
    public class AddLifeCommand : ICommand
    {
        private readonly HealthSystem _healthSystem;
        public AddLifeCommand(HealthSystem healthSystem) => _healthSystem = healthSystem;
        public void Execute() => _healthSystem.AddHealth();
    }
}