namespace Game.Base
{
    public interface IGameFactory<in TRequires, out TGame>
        where TRequires : IGameRequires
        where TGame : IGame
    {
        void SetupGameRequires(TRequires gameRequires);
        TGame CreateGame();
    }
    
    public interface IGameRequires { }
}