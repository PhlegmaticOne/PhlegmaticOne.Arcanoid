using System;

namespace Game.Base
{
    public interface IGame { }
    public interface IGame<in TData, out TEvents> : IGame
    {
        TEvents Events { get; }
        event Action Won;
        event Action Lost;
        void StartGame(TData data);
        void Pause();
        void Unpause();
        void Stop();
    }
    
    public interface IGameEvents { }
    public interface IGameData { }
}