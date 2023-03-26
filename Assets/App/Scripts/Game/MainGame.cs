using System;
using DG.Tweening;
using Game.Base;
using Game.Composites;
using Game.Field;
using Game.Field.Builder;
using Game.GameEntities.Blocks;
using Game.GameEntities.PlayerObjects.BallObject;
using Game.GameEntities.PlayerObjects.BallObject.Spawners;
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
        private readonly EntitiesOnFieldCollection _entitiesOnFieldCollection;
        private readonly GameSystems _gameSystems;
        private readonly IFieldBuilder _fieldBuilder;
        private readonly IBallSpawner _ballSpawner;
        private readonly TimeActionsManager _timeActionsManager;
        private readonly ParticleManager _particleManager;

        private GameField _gameField;
        private StateCheckSystem _stateCheckSystem;
        private float _slowDownTime;

        public MainGame(IPoolProvider poolProvider,
            EntitiesOnFieldCollection entitiesOnFieldCollection,
            GameSystems gameSystems,
            IFieldBuilder fieldBuilder,
            TimeActionsManager timeActionsManager,
            ParticleManager particleManager,
            IBallSpawner ballSpawner)
        {
            _poolProvider = poolProvider;
            _entitiesOnFieldCollection = entitiesOnFieldCollection;
            _gameSystems = gameSystems;
            _fieldBuilder = fieldBuilder;
            _timeActionsManager = timeActionsManager;
            _particleManager = particleManager;
            _ballSpawner = ballSpawner;
            Events = new MainGameEvents();
        }

        public void SetParameters(float slowDownTime) => _slowDownTime = slowDownTime;

        public MainGameEvents Events { get; }
        public event Action Won;
        public event Action PreWon;
        public event Action Lost;
        public event Action Started;
        public event Action Initialized;

        public void StartGame(MainGameData data)
        {
            var controlSystem = _gameSystems.ControlSystem;
            var ballsOnField = _entitiesOnFieldCollection.BallsOnField;
            
            var ball = _ballSpawner.CreateBall(new BallCreationContext
            {
                Position = Vector2.zero,
                SetSpecifiedStartSpeed = false
            });
            ballsOnField.Add(ball);
            controlSystem.ReturnToPosition();
            controlSystem.AddObjectToFollow(ball);
            controlSystem.EnableInput();
            
            _gameSystems.HealthSystem.Initialize(data.LevelData.LifesCount);
            _gameSystems.CaptiveBallsSystem.Initialize(_poolProvider, ballsOnField);
            Initialized?.Invoke();
            BuildField(data);
        }
        
        public void Pause()
        {
            _gameSystems.ControlSystem.DisableInput();
            SetTimeScale(0);
        }

        public void Unpause()
        {
            _gameSystems.ControlSystem.EnableInput();
            SetTimeScale(1);
        }
        
        public void Stop()
        {
            var blocksPool = _poolProvider.GetPool<Block>();
            foreach (var block in _gameField.Blocks)
            {
                if (block != null && block.IsDestroyed == false)
                {
                    blocksPool.ReturnToPool(block);
                }
            }
            
            _entitiesOnFieldCollection.ReturnToPool(_poolProvider);
            _gameField.Clear();
            
            _particleManager.Disable();
            _timeActionsManager.StopAllActions();
            _gameSystems.Disable();
            Unsubscribe();
            SetTimeScale(1);
        }

        private void BuildField(MainGameData data)
        {
            _gameField = _fieldBuilder.BuildField(data.LevelData);
            _stateCheckSystem = new StateCheckSystem(_gameField);
            _fieldBuilder.FieldBuilt += FieldBuilderOnFieldBuilt;
            Subscribe();
        }

        private void FieldBuilderOnFieldBuilt()
        {
            _gameSystems.ControlSystem.Enable(false);
            _fieldBuilder.FieldBuilt -= FieldBuilderOnFieldBuilt;
            Started?.Invoke();
        }

        private void BallsOnFieldOnBallRemoved(Ball ball)
        {
            if (_entitiesOnFieldCollection.BallsOnField.All.Count == 0 &&
                _gameSystems.HealthSystem.CurrentHealth == 0)
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

        private void StateCheckSystemOnActiveBlocksDestroyed()
        {
            PreWon?.Invoke();
            DOVirtual
                .Float(1f, 0f, _slowDownTime, SetTimeScale)
                .SetUpdate(true)
                .SetEase(Ease.OutSine)
                .OnComplete(() => Won?.Invoke())
                .Play();
        }

        private void SetTimeScale(float scale)
        {
            Time.timeScale = scale;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }
        
        private void Subscribe()
        {
            var healthSystem = _gameSystems.HealthSystem;
            _stateCheckSystem.ActiveBlocksDestroyed += StateCheckSystemOnActiveBlocksDestroyed;
            _gameField.BlockRemoved += GameFieldOnBlockRemoved;
            healthSystem.HealthAdded += HealthSystemOnHealthAdded;
            healthSystem.HealthLost += HealthSystemOnHealthLost;
            _entitiesOnFieldCollection.BallsOnField.BallRemoved += BallsOnFieldOnBallRemoved;
        }

        private void Unsubscribe()
        {
            var healthSystem = _gameSystems.HealthSystem;
            _stateCheckSystem.ActiveBlocksDestroyed -= StateCheckSystemOnActiveBlocksDestroyed;
            _gameField.BlockRemoved -= GameFieldOnBlockRemoved;
            healthSystem.HealthAdded -= HealthSystemOnHealthAdded;
            healthSystem.HealthLost -= HealthSystemOnHealthLost;
            _entitiesOnFieldCollection.BallsOnField.BallRemoved -= BallsOnFieldOnBallRemoved;
        }
    }
}