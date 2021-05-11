using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using TeamManagement.DataLayer.Domain.Models;
using TeamManagement.DataLayer.Data;

namespace TeamManagement.Installers
{
    public class IdentityInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<AppUser, IdentityRole>(options =>
                options.SignIn.RequireConfirmedAccount = false)
                    .AddEntityFrameworkStores<AppDbContext>();
        }
    }
}
