using System;

namespace Game.Base
{
    public interface IGame
    {
        event Action Won;
        event Action Lost;
        event Action Started;
        event Action Initialized;
        void Pause();
        void Unpause();
        void Stop();
    }
    
    public interface IGame<in TData, out TEvents> : IGame
        where TEvents : IGameEvents
        where TData : IGameData
    {
        TEvents Events { get; }
        void StartGame(TData data);
    }
    
    public interface IGameEvents { }
    public interface IGameData { }
}