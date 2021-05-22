using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using TeamManagement.DataLayer.Domain.Models.Auth;
using TeamManagement.Installers;
using TeamManagement.Options;

namespace PortalForArbitrators
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            InstallerExtensions.InstallServicesInAssembly(services, Configuration);
            services.Configure<StripeKeys>(Configuration.GetSection("StripeKeys"));
            services.AddMvc().AddNewtonsoftJson(o =>
            {
                o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Login/Account/";

                });

            //var authOptions = Configuration.GetSection(nameof(TeamManagement.BusinessLayer.Options.AuthOptions)).Get<TeamManagement.BusinessLayer.Options.AuthOptions>();


            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(o =>
            //{
            //    o.IncludeErrorDetails = true;

            //    o.TokenValidationParameters = new TokenValidationParameters()
            //    {
            //        ValidateActor = false,
            //        ValidIssuer = AuthOptions.ISSUER,
            //        ValidAudience = AuthOptions.AUDIENCE,
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            //        ValidateLifetime = true,
            //    };
            //});

            //services.AddAuthentication().AddGoogle(options =>
            //{
            //    options.ClientId = authOptions.ClientID;
            //    options.ClientSecret = authOptions.ClientSecret;

            //});

            services.AddAuthorization();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var swaggerOptions = new SwaggerOptions();

            Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);

            app.UseSwagger(options =>
            {
                options.RouteTemplate = swaggerOptions.JsonRoute;
            });

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint(swaggerOptions.UIEndpoint, swaggerOptions.Description);
            });

            var allowedOriginsCORS = Configuration.GetSection("AllowedOriginsCORS").Get<string[]>();
            app.UseCors(builder =>
            {
                builder.WithOrigins(allowedOriginsCORS)
                    .AllowCredentials();
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCookiePolicy();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
