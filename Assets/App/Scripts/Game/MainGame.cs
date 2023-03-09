using System;
using Game.Base;
using Game.Blocks;
using Game.Field;
using Game.Field.Builder;
using Game.Field.Helpers;
using Game.PlayerObjects.BallObject;
using Game.PlayerObjects.BallObject.Spawners;
using Game.PlayerObjects.ShipObject;
using Game.Systems.Control;
using Libs.Pooling.Base;
using UnityEngine;

namespace Game
{
    public class MainGame : IGame<MainGameData, MainGameEvents>
    {
        private readonly IPoolProvider _poolProvider;
        private readonly IFieldBuilder _fieldBuilder;
        private readonly InteractableZoneSetter _interactableZoneSetter;
        private readonly ControlSystem _controlSystem;
        private readonly IBallSpawner _ballSpawner;
        private readonly Ship _ship;

        private GameField _gameField;
        private Ball _ball;

        public MainGame(IPoolProvider poolProvider,
            IFieldBuilder fieldBuilder, 
            InteractableZoneSetter interactableZoneSetter,
            ControlSystem controlSystem,
            IBallSpawner ballSpawner, 
            Ship ship)
        {
            _poolProvider = poolProvider;
            _fieldBuilder = fieldBuilder;
            _interactableZoneSetter = interactableZoneSetter;
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
            var interactableBounds = _interactableZoneSetter.CalculateZoneBounds(_gameField.Bounds);
            _interactableZoneSetter.SetInteractableZone(interactableBounds);
            _controlSystem.SetInteractableBounds(interactableBounds);
            _controlSystem.Enable();
            _ship.Enable();
            _ball = _ballSpawner.CreateBall(new BallCreationContext(Vector2.zero, 5));
            _controlSystem.AddObjectToFollow(_ball);
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
            
            foreach (var block in _gameField.Blocks)
            {
                if (block.IsDestroyed == false)
                {
                    blocksPool.ReturnToPool(block);
                }
            }
            
            ballsPool.ReturnToPool(_ball);
            _controlSystem.Disable();
            SetTimeScale(1);
        }

        private void SetTimeScale(float scale)
        {
            Time.timeScale = scale;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }
    }
}