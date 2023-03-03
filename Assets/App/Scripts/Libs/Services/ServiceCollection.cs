using System;
using System.Collections.Generic;

namespace Libs.Services
{
    public class ServiceCollection : IServiceCollection
    {
        private readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();
        
        public IServiceCollection AddSingleton<TService>(TService service)
        {
            _services.Add(typeof(TService), service);
            return this;
        }

        public IServiceProvider BuildServiceProvider() => new ServiceProvider(_services);
    }
}