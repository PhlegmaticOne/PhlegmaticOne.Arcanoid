namespace Abstracts.Services
{
    public interface IServiceProvider
    {
        TService GetRequiredService<TService>();
    }
}