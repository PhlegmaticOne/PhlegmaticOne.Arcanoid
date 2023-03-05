using System;
using Game.Base;
using Game.Field.Builder;
using Game.PlayerObjects;
using Game.PlayerObjects.BallObject.Factories;
using UnityEngine;

namespace Game
{
    public class MainGame : IGame<MainGameData, MainGameEvents>
    {
        private readonly IFieldBuilder _fieldBuilder;
        private readonly IBallSpawner _ballSpawner;
        private readonly Racket _racket;

        public MainGame(IFieldBuilder fieldBuilder, IBallSpawner ballSpawner, Racket racket)
        {
            _fieldBuilder = fieldBuilder;
            _ballSpawner = ballSpawner;
            _racket = racket;
            Events = new MainGameEvents();
        }

        public MainGameEvents Events { get; }
        public event Action Won;
        public event Action Lost;
        
        public void StartGame(MainGameData data)
        {
            _fieldBuilder.BuildField(data.LevelData);
            _racket.gameObject.SetActive(true);
            var pos = _racket.transform.position + new Vector3(0, 1, 0);
            var ball = _ballSpawner.CreateBall(new BallCreationContext(pos, 4));
            ball.StartMove();
        }
    }
}