namespace Libs.Services
{
    public interface IServiceProvider
    {
        TService GetRequiredService<TService>();
    }
}