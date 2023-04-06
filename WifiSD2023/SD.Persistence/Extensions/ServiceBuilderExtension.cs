using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Wifi.SD.Core.Attributes;

namespace SD.Persistence.Extensions
{
    public static class ServiceBuilderExtension
    {
        public static void RegisterRepositories(this IServiceCollection service)
        {
            service.Scan(scan => scan
            .FromAssemblies(Assembly.GetExecutingAssembly())
            .AddClasses(c => c.WithAttribute<MapServiceDependencyAttribute>())
            .AsImplementedInterfaces()
            .WithScopedLifetime()
            );
        }
    }
}
