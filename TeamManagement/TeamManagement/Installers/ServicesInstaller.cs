using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TeamManagement.BusinessLayer.Services;
using TeamManagement.BusinessLayer.Services.Interfaces;
using TeamManagement.Contracts.v1.Requests;
using TeamManagement.DataLayer.Domain.Models;
using TeamManagement.DataLayer.Repositories;
using TeamManagement.DataLayer.Repositories.Interfaces;
using TeamManagement.Validators;

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

            services.AddTransient<IGenericRepository<Poll>, BaseGenericRepository<Poll>>();
            services.AddTransient<AbstractValidator<ArticleCreateRequest>, ArticleCreateRequestValidator>();
            services.AddTransient<AbstractValidator<ArticleUpdateRequest>, ArticleUpdateRequestValidator>();
            services.AddTransient<AbstractValidator<HowToArticleCreateRequest>, HowToArticleCreateRequestValidator>();
            services.AddTransient<AbstractValidator<HowToArticleUpdateRequest>, HowToArticleUpdateRequestValidator>();
            services.AddTransient<AbstractValidator<HowToArticleUpdateRequest>, HowToArticleUpdateRequestValidator>();
            services.AddTransient<AbstractValidator<CreatePollRequest>, CreatePollRequestValidator>();
            services.AddTransient<AbstractValidator<TagCreateRequest>, TagCreateRequestValidator>();
            services.AddTransient<ICompanyRepository, CompanyRepository>();        }
    }
}
