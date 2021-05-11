using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PortalForArbitrators;
using System;

namespace TeamManagement.Installers
{
    public class AutoMapperInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}
