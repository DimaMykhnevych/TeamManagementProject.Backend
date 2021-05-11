using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PortalForArbitrators;
using System;
using System.Linq;
using System.Reflection;

namespace TeamManagement.Installers
{
    public static class InstallerExtensions
    {
        public static void InstallServicesInAssembly(this IServiceCollection services, IConfiguration Configuration)
        {
            Assembly.GetAssembly(typeof(Startup))?
                .GetTypes()
                .Where(x => !x.IsInterface && !x.IsAbstract && typeof(IInstaller).IsAssignableFrom(x))
                .Select(Activator.CreateInstance)
                .Cast<IInstaller>()
                .ToList()
                .ForEach(installer => installer.InstallServices(services, Configuration));
        }
    }
}
