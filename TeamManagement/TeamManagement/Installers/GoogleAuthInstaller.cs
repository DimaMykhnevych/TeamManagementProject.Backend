using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TeamManagement.BusinessLayer.Options;

namespace TeamManagement.Installers
{
    public class GoogleAuthInstaller:IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var authOptions = configuration.GetSection(nameof(AuthOptions)).Get<AuthOptions>();
            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.ClientId = authOptions.ClientID;
                    options.ClientSecret = authOptions.ClientSecret;
                });
        }
    }
}
