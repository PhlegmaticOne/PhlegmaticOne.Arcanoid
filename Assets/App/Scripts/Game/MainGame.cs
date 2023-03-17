using System;
using Game.Base;
using Game.Field;
using Game.Field.Builder;
using Game.GameEntities.Blocks;
using Game.GameEntities.Bonuses;
using Game.GameEntities.Bonuses.Behaviors.CaptiveBall;
using Game.GameEntities.PlayerObjects.BallObject;
using Game.GameEntities.PlayerObjects.BallObject.Spawners;
using Game.GameEntities.PlayerObjects.ShipObject;
using Game.Logic.Systems.Control;
using Game.Logic.Systems.Health;
using Game.Logic.Systems.StateCheck;
using Game.ObjectParticles;
using Libs.Pooling.Base;
using Libs.TimeActions;
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
        private readonly TimeActionsManager _timeActionsManager;
        private readonly CaptiveBallsSystem _captiveBallsSystem;
        private readonly ParticleManager _particleManager;

        private GameField _gameField;
        private StateCheckSystem _stateCheckSystem;

        public MainGame(IPoolProvider poolProvider,
            IFieldBuilder fieldBuilder,
            TimeActionsManager timeActionsManager,
            HealthSystem healthSystem,
            BallsOnField ballsOnField,
            BonusesOnField bonusesOnField,
            ControlSystem controlSystem,
            CaptiveBallsSystem captiveBallsSystem,
            ParticleManager particleManager,
            IBallSpawner ballSpawner, 
            Ship ship)
        {
            _poolProvider = poolProvider;
            _fieldBuilder = fieldBuilder;
            _timeActionsManager = timeActionsManager;
            _healthSystem = healthSystem;
            _ballsOnField = ballsOnField;
            _bonusesOnField = bonusesOnField;
            _controlSystem = controlSystem;
            _captiveBallsSystem = captiveBallsSystem;
            _particleManager = particleManager;
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
            
            var ball = _ballSpawner.CreateBall(new BallCreationContext
            {
                Position = Vector2.zero,
                SetSpecifiedStartSpeed = false
            });
            _ballsOnField.AddBall(ball);
            _controlSystem.AddObjectToFollow(ball);
            
            _healthSystem.Initialize(data.LevelData.LifesCount);
            _captiveBallsSystem.Initialize(_poolProvider, _ballsOnField);
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
            
            
            _particleManager.Disable();
            _timeActionsManager.StopAllActions();
            _controlSystem.Disable();
            _captiveBallsSystem.Disable();
            Unsubscribe();
            SetTimeScale(1);
        }
        
        private void BallsOnFieldOnBallRemoved(Ball ball)
        {
            if (_ballsOnField.All.Count == 0 && _healthSystem.CurrentHealth == 0)
            {
                Lost?.Invoke();
            }
        }
        
        private void HealthSystemOnHealthLost() => Events.OnLoseHealth();

        private void HealthSystemOnHealthAdded() => Events.OnAddHealth();

        private void GameFieldOnBlockRemoved(Block block)
        {
            Events.OnBlockDestroyed(new BlockDestroyedEventArgs
            {
                ActiveBlocksCount = _gameField.StartDefaultBlocksCount,
                RemainBlocksCount = _gameField.GetDefaultBlocksCount()
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
            _ballsOnField.BallRemoved += BallsOnFieldOnBallRemoved;
        }

        private void Unsubscribe()
        {
            _stateCheckSystem.ActiveBlocksDestroyed -= StateCheckSystemOnActiveBlocksDestroyed;
            _gameField.BlockRemoved -= GameFieldOnBlockRemoved;
            _healthSystem.HealthAdded -= HealthSystemOnHealthAdded;
            _healthSystem.HealthLost -= HealthSystemOnHealthLost;
            _ballsOnField.BallRemoved -= BallsOnFieldOnBallRemoved;
        }
    }
}