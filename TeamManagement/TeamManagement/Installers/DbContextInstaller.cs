using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TeamManagement.DataLayer.Data;

namespace TeamManagement.Installers
{
    public class DbContextInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            string connectionsString = configuration.GetConnectionString("SqlConnection");
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionsString));
        }
    }
}
