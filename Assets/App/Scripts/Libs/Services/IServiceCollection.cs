using System;

namespace Libs.Services
{
    public interface IServiceCollection
    {
        IServiceCollection AddSingleton<TService>(TService service);
        IServiceCollection AddSingleton<TService>(Func<IServiceProvider, TService> factoryFunc);
        IServiceCollection AddTransient<TService>(Func<IServiceProvider, TService> factoryFunc);
        IServiceProvider BuildServiceProvider();
    }
}