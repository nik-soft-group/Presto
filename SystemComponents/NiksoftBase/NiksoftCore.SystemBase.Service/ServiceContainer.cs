using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using NiksoftCore.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NiksoftCore.SystemBase.Service
{
    public static class ServiceContainer
    {
        public static ISystemUnitOfWork GetInstanceContext()
        {
            ISystemUnitOfWork uow = new SystemBaseDbContext();
            return uow;
        }

        public static ContainerBuilder GetServcie(IServiceCollection services)
        {
            var builder = new ContainerBuilder();

            builder.Populate(services);

            Assembly webAssembly = Assembly.GetExecutingAssembly();
            Assembly coreAssembly = Assembly.GetAssembly(typeof(SystemSetting));
            Assembly serviceAss = Assembly.GetAssembly(typeof(SystemSettingService));
            builder.RegisterAssemblyTypes(webAssembly, coreAssembly, serviceAss).AsImplementedInterfaces();

            builder.RegisterType<SystemSettingService>().As<ISystemSettingService>();
            return builder;
        }

    }
}
