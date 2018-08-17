using System;
using System.Collections.Generic;

namespace ERP.Common.Infrastructure.Ioc
{
     public interface ILifetimeScope : IDisposable
    {
        TService Resolve<TService>();

        TService Resolve<TService>(string serviceName);

        TService Resolve<TService>(IEnumerable<Autofac.Core.Parameter> parameters);

        TService Resolve<TService>(params Autofac.Core.Parameter[] parameters);
    }
}