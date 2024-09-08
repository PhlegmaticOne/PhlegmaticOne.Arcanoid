using System;
using System.Collections.Generic;

namespace Libs.Services
{
    public class ServiceProvider : IServiceProvider
    {
        private readonly Dictionary<Type, object> _services;
        private readonly Dictionary<Type, Func<IServiceProvider, object>> _factoryFuncs;
        private readonly Dictionary<Type, Func<IServiceProvider, object>> _transientFuncs;

        public ServiceProvider(Dictionary<Type, object> services, 
            Dictionary<Type, Func<IServiceProvider, object>> factoryFuncs,
            Dictionary<Type, Func<IServiceProvider, object>> transientFuncs)
        {
            _services = services;
            _factoryFuncs = factoryFuncs;
            _transientFuncs = transientFuncs;
        }

        public TService GetRequiredService<TService>()
        {
            var type = typeof(TService);
            
            if (_transientFuncs.TryGetValue(type, out var transientFunc))
            {
                return (TService)transientFunc(this);
            }
            
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

        public object GetService(Type serviceType)
        {
            if (_transientFuncs.TryGetValue(serviceType, out var transientFunc))
            {
                return transientFunc(this);
            }
            
            if (_services.TryGetValue(serviceType, out var service))
            {
                return service;
            }

            if (_factoryFuncs.TryGetValue(serviceType, out var factoryFunc))
            {
                var created = factoryFunc(this);
                _services.Add(serviceType, created);
                _factoryFuncs.Remove(serviceType);
                return created;
            }

            return null;
        }
    }
}