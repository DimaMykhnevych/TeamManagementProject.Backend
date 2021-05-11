using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TeamManagement.Installers
{
    public class MvcInstaller:IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
        }
    }
}
