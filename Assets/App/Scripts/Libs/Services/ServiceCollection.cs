using System;
using System.Collections.Generic;

namespace Libs.Services
{
    public class ServiceCollection : IServiceCollection
    {
        private readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();

        private readonly Dictionary<Type, Func<IServiceProvider, object>> _factoryFuncs =
            new Dictionary<Type, Func<IServiceProvider, object>>();

        private readonly Dictionary<Type, Func<IServiceProvider, object>> _transientFuncs =
            new Dictionary<Type, Func<IServiceProvider, object>>();

        public IServiceCollection AddSingleton<TService>(TService service)
        {
            _services.Add(typeof(TService), service);
            return this;
        }

        public IServiceCollection AddSingleton<TService>(Func<IServiceProvider, TService> factoryFunc)
        {
            _factoryFuncs.Add(typeof(TService), s => factoryFunc(s));
            return this;
        }

        public IServiceCollection AddTransient<TService>(Func<IServiceProvider, TService> factoryFunc)
        {
            _transientFuncs.Add(typeof(TService), s => factoryFunc(s));
            return this;
        }

        public IServiceProvider BuildServiceProvider() => new ServiceProvider(_services, _factoryFuncs, _transientFuncs);
    }
}