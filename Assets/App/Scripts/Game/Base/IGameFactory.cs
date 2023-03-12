namespace Game.Base
{
    public interface IGameFactory<out TGame> where TGame : IGame
    {
        TGame CreateGame();
    }
}