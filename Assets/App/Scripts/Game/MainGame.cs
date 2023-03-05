using System;
using Game.Base;
using Game.Field.Builder;
using Game.PlayerObjects.BallObject.Factories;
using Game.PlayerObjects.ShipObject;
using Game.Systems.Control;
using UnityEngine;

namespace Game
{
    public class MainGame : IGame<MainGameData, MainGameEvents>
    {
        private readonly IFieldBuilder _fieldBuilder;
        private readonly ControlSystem _controlSystem;
        private readonly IBallSpawner _ballSpawner;
        private readonly Ship _ship;

        public MainGame(IFieldBuilder fieldBuilder, 
            ControlSystem controlSystem,
            IBallSpawner ballSpawner, 
            Ship ship)
        {
            _fieldBuilder = fieldBuilder;
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
            _fieldBuilder.BuildField(data.LevelData);
            
            _ship.Enable();
            var ball = _ballSpawner.CreateBall(new BallCreationContext(Vector2.zero, 4));
            _controlSystem.AddObjectToFollow(ball);
        }
    }
}