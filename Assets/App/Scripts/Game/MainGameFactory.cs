using Game.Base;
using Game.Field.Builder;
using Game.PlayerObjects.BallObject;
using Game.PlayerObjects.BallObject.Spawners;
using Game.PlayerObjects.ShipObject;
using Game.Systems.Control;
using Game.Systems.Health;
using Libs.InputSystem;
using Libs.Pooling.Base;

namespace Game
{
    public class MainGameFactory : IGameFactory<MainGame>
    {
        private readonly IInputSystem _inputSystem;
        private readonly ControlSystem _controlSystem;
        private readonly HealthSystem _healthSystem;
        private readonly BallsOnField _ballsOnField;
        private readonly Ship _ship;
        private readonly IFieldBuilder _fieldBuilder;
        private readonly IPoolProvider _poolProvider;
        private readonly IBallSpawner _ballSpawner;

        public MainGameFactory(IFieldBuilder fieldBuilder, IPoolProvider poolProvider,
            IBallSpawner ballSpawner, IInputSystem inputSystem,
            ControlSystem controlSystem, HealthSystem healthSystem,
            BallsOnField ballsOnField, Ship ship)
        {
            _fieldBuilder = fieldBuilder;
            _poolProvider = poolProvider;
            _ballSpawner = ballSpawner;
            _inputSystem = inputSystem;
            _controlSystem = controlSystem;
            _healthSystem = healthSystem;
            _ballsOnField = ballsOnField;
            _ship = ship;
        }
        
        public MainGame CreateGame()
        {
            _controlSystem.Initialize(_inputSystem, _ship);
            return new MainGame(_poolProvider, _fieldBuilder,
                _healthSystem, _ballsOnField,
                _controlSystem, _ballSpawner, _ship);
        }
    }
}