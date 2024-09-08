namespace Game.GameEntities.PlayerObjects.Base
{
    public interface IControlable : IDimensionable
    {
        float ControlLerp { get; }
    }
}