using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TeamManagement.BusinessLayer.Services;
using TeamManagement.BusinessLayer.Services.Interfaces;
using TeamManagement.DataLayer.Domain.Models;
using TeamManagement.DataLayer.Repositories;
using TeamManagement.DataLayer.Repositories.Interfaces;

namespace TeamManagement.Installers
{
    public class ServicesInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<ICompanyService, CompanyService>();
            services.AddTransient<IUserService, UserService>();

            services.AddTransient<IGenericRepository<Article>, BaseGenericRepository<Article>>();
            services.AddTransient<IHowToArticlesRepository, HowToArticlesRepository>();
            services.AddTransient<IGenericRepository<Tag>, BaseGenericRepository<Tag>>();
            services.AddTransient<ICompanyRepository, CompanyRepository>();
        }
    }
}
