namespace Abstracts.Services
{
    public interface IServiceCollection
    {
        IServiceCollection AddSingleton<TService>(TService service);
        IServiceProvider BuildServiceProvider();
    }
}