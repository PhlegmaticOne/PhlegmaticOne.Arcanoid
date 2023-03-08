using System;
using System.Collections.Generic;

namespace Libs.Services
{
    public class ServiceProvider : IServiceProvider
    {
        private readonly Dictionary<Type, object> _services;
        private readonly Dictionary<Type, Func<IServiceProvider, object>> _factoryFuncs;

        public ServiceProvider(Dictionary<Type, object> services, Dictionary<Type, Func<IServiceProvider, object>> factoryFuncs)
        {
            _services = services;
            _factoryFuncs = factoryFuncs;
        }

        public TService GetRequiredService<TService>()
        {
            var type = typeof(TService);
            
            if (_services.TryGetValue(type, out var service))
            {
                return (TService)service;
            }

            if (_factoryFuncs.TryGetValue(type, out var factoryFunc))
            {
                var created = (TService)factoryFunc(this);
                _services.Add(type, created);
                _factoryFuncs.Remove(type);
                return created;
            }

            throw new ArgumentException("Service is not registered", typeof(TService).Name);
        }
    }
}