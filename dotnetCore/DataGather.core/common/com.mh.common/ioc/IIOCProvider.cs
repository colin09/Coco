using System;
using Microsoft.Extensions.DependencyInjection;

namespace com.mh.common.ioc
{

    public sealed class IocProvider
    {

        private static IServiceProvider _container;
        private static IServiceCollection _collection = null;

        public static IServiceProvider Container
        {
            get { return _container; }
        }

        public static void RegisterServices(Action<IServiceCollection> register)
        {
            if (_collection == null)
                _collection = new ServiceCollection();

            register(_collection);

            _container = _collection.BuildServiceProvider();
        }

        public static TService GetService<TService>()
        {
            return _container.GetRequiredService<TService>();
        }


    }
}