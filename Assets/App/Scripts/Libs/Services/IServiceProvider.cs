using System;

namespace Libs.Services
{
    public interface IServiceProvider
    {
        TService GetRequiredService<TService>();
        object GetService(Type serviceType);
    }
}