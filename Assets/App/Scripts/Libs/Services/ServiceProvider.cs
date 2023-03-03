using System;
using System.Collections.Generic;

namespace Libs.Services
{
    public class ServiceProvider : IServiceProvider
    {
        private readonly Dictionary<Type, object> _services;

        public ServiceProvider(Dictionary<Type, object> services) => _services = services;

        public TService GetRequiredService<TService>() => (TService)_services[typeof(TService)];
    }
}