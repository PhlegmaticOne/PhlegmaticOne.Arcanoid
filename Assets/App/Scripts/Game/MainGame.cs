using System;
using Game.Base;
using Game.Field.Builder;

namespace Game
{
    public class MainGame : IGame<MainGameData, MainGameEvents>
    {
        private readonly IFieldBuilder _fieldBuilder;
        public MainGame(IFieldBuilder fieldBuilder)
        {
            _fieldBuilder = fieldBuilder;
            Events = new MainGameEvents();
        }

        public MainGameEvents Events { get; }
        public event Action Won;
        public event Action Lost;
        
        public void StartGame(MainGameData data)
        {
            _fieldBuilder.BuildField(data.LevelData);
        }
    }
}