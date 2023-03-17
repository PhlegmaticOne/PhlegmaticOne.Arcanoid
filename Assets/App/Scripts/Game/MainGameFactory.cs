using Game.Base;
using Game.Field.Builder;
using Game.GameEntities.Bonuses;
using Game.GameEntities.Bonuses.Behaviors.CaptiveBall;
using Game.GameEntities.PlayerObjects.BallObject;
using Game.GameEntities.PlayerObjects.BallObject.Spawners;
using Game.GameEntities.PlayerObjects.ShipObject;
using Game.Logic.Systems.Control;
using Game.Logic.Systems.Health;
using Libs.InputSystem;
using Libs.Pooling.Base;
using Libs.TimeActions;

namespace Game
{
    public class MainGameFactory : IGameFactory<MainGame>
    {
        private readonly IInputSystem _inputSystem;
        private readonly ControlSystem _controlSystem;
        private readonly CaptiveBallsSystem _captiveBallsSystem;
        private readonly HealthSystem _healthSystem;
        private readonly BallsOnField _ballsOnField;
        private readonly BonusesOnField _bonusesOnField;
        private readonly Ship _ship;
        private readonly IFieldBuilder _fieldBuilder;
        private readonly IPoolProvider _poolProvider;
        private readonly TimeActionsManager _timeActionsManager;
        private readonly IBallSpawner _ballSpawner;

        public MainGameFactory(IFieldBuilder fieldBuilder, IPoolProvider poolProvider,
            TimeActionsManager timeActionsManager,
            IBallSpawner ballSpawner, IInputSystem inputSystem,
            ControlSystem controlSystem, CaptiveBallsSystem captiveBallsSystem, HealthSystem healthSystem,
            BallsOnField ballsOnField, BonusesOnField bonusesOnField, Ship ship)
        {
            _fieldBuilder = fieldBuilder;
            _poolProvider = poolProvider;
            _timeActionsManager = timeActionsManager;
            _ballSpawner = ballSpawner;
            _inputSystem = inputSystem;
            _controlSystem = controlSystem;
            _captiveBallsSystem = captiveBallsSystem;
            _healthSystem = healthSystem;
            _ballsOnField = ballsOnField;
            _bonusesOnField = bonusesOnField;
            _ship = ship;
        }
        
        public MainGame CreateGame()
        {
            _controlSystem.Initialize(_inputSystem, _ship);
            return new MainGame(_poolProvider, _fieldBuilder, _timeActionsManager,
                _healthSystem, _ballsOnField, _bonusesOnField,
                _controlSystem, _captiveBallsSystem, _ballSpawner, _ship);
        }
    }
}