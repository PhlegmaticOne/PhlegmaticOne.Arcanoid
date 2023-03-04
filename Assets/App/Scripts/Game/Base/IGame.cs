using System;

namespace Game.Base
{
    public interface IGame<in TData, out TEvents>
    {
        TEvents Events { get; }
        event Action Won;
        event Action Lost;
        void StartGame(TData data);
    }
    
    public interface IGameEvents { }
    public interface IGameData { }
}