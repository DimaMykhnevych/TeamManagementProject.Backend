using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace TeamManagement.Installers
{
    public class GoogleAuthInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var authOptions = configuration.GetSection(nameof(BusinessLayer.Options.AuthOptions)).Get<BusinessLayer.Options.AuthOptions>();

            services.AddAuthentication().AddGoogle(options =>
            {
                options.ClientId = authOptions.ClientID;
                options.ClientSecret = authOptions.ClientSecret;

            });
        }
    }
}
