using System;
using Game.Base;
using Game.Blocks;
using Game.Bonuses;
using Game.Field;
using Game.Field.Builder;
using Game.PlayerObjects.BallObject;
using Game.PlayerObjects.BallObject.Spawners;
using Game.PlayerObjects.ShipObject;
using Game.Systems.Control;
using Game.Systems.Health;
using Game.Systems.StateCheck;
using Libs.Pooling.Base;
using UnityEngine;

namespace Game
{
    public class MainGame : IGame<MainGameData, MainGameEvents>
    {
        private readonly IPoolProvider _poolProvider;
        private readonly IFieldBuilder _fieldBuilder;
        private readonly HealthSystem _healthSystem;
        private readonly BallsOnField _ballsOnField;
        private readonly BonusesOnField _bonusesOnField;
        private readonly ControlSystem _controlSystem;
        private readonly IBallSpawner _ballSpawner;
        private readonly Ship _ship;

        private GameField _gameField;
        private Ball _ball;
        private StateCheckSystem _stateCheckSystem;

        public MainGame(IPoolProvider poolProvider,
            IFieldBuilder fieldBuilder,
            HealthSystem healthSystem,
            BallsOnField ballsOnField,
            BonusesOnField bonusesOnField,
            ControlSystem controlSystem,
            IBallSpawner ballSpawner, 
            Ship ship)
        {
            _poolProvider = poolProvider;
            _fieldBuilder = fieldBuilder;
            _healthSystem = healthSystem;
            _ballsOnField = ballsOnField;
            _bonusesOnField = bonusesOnField;
            _controlSystem = controlSystem;
            _ballSpawner = ballSpawner;
            _ship = ship;
            Events = new MainGameEvents();
        }

        public MainGameEvents Events { get; }
        public event Action Won;
        public event Action Lost;
        
        public void StartGame(MainGameData data)
        {
            _gameField = _fieldBuilder.BuildField(data.LevelData);
            _stateCheckSystem = new StateCheckSystem(_gameField);
            _controlSystem.Enable();
            _ship.Enable();
            _ball = _ballSpawner.CreateBall(new BallCreationContext
            {
                Position = Vector2.zero,
                SetSpecifiedStartSpeed = false
            });
            _ballsOnField.AddBall(_ball);
            _controlSystem.AddObjectToFollow(_ball);
            _healthSystem.Initialize(data.LevelData.LifesCount);
            Subscribe();
        }

        public void Pause()
        {
            _controlSystem.DisableInput();
            SetTimeScale(0);
        }

        public void Unpause()
        {
            _controlSystem.EnableInput();
            SetTimeScale(1);
        }

        public void Stop()
        {
            var blocksPool = _poolProvider.GetPool<Block>();
            var ballsPool = _poolProvider.GetPool<Ball>();
            var bonusesPool = _poolProvider.GetPool<Bonus>();
            
            foreach (var block in _gameField.Blocks)
            {
                if (block.IsDestroyed == false)
                {
                    blocksPool.ReturnToPool(block);
                }
            }

            foreach (var ball in _ballsOnField.All)
            {
                ballsPool.ReturnToPool(ball);
            }
            
            foreach (var bonus in _bonusesOnField.All)
            {
                bonusesPool.ReturnToPool(bonus);
            }
            
            _ballsOnField.Clear();
            _bonusesOnField.Clear();
            _gameField.Clear();
            
            _controlSystem.Disable();
            Unsubscribe();
            SetTimeScale(1);
        }
        
        private void HealthSystemOnAllHealthLost() => Lost?.Invoke();

        private void HealthSystemOnHealthLost() => Events.OnLoseHealth();

        private void HealthSystemOnHealthAdded() => Events.OnAddHealth();

        private void GameFieldOnBlockRemoved(Block block)
        {
            Events.OnBlockDestroyed(new BlockDestroyedEventArgs
            {
                ActiveBlocksCount = _gameField.StartActiveBlocksCount,
                RemainBlocksCount = _gameField.ActiveBlocksCount
            });
        }

        private void StateCheckSystemOnActiveBlocksDestroyed() => Won?.Invoke();

        private void SetTimeScale(float scale)
        {
            Time.timeScale = scale;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }

        private void Subscribe()
        {
            _stateCheckSystem.ActiveBlocksDestroyed += StateCheckSystemOnActiveBlocksDestroyed;
            _gameField.BlockRemoved += GameFieldOnBlockRemoved;
            _healthSystem.HealthAdded += HealthSystemOnHealthAdded;
            _healthSystem.HealthLost += HealthSystemOnHealthLost;
            _healthSystem.AllHealthLost += HealthSystemOnAllHealthLost;
        }
        
        private void Unsubscribe()
        {
            _stateCheckSystem.ActiveBlocksDestroyed -= StateCheckSystemOnActiveBlocksDestroyed;
            _gameField.BlockRemoved -= GameFieldOnBlockRemoved;
            _healthSystem.HealthAdded -= HealthSystemOnHealthAdded;
            _healthSystem.HealthLost -= HealthSystemOnHealthLost;
            _healthSystem.AllHealthLost -= HealthSystemOnAllHealthLost;
        }
    }
}