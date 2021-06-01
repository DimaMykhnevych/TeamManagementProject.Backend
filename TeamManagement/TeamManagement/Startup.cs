using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
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


            services.ConfigureApplicationCookie(options =>
            {
                //options.LoginPath = "/Login/Account/";
                //options.Cookie.HttpOnly = false;
                //options.Cookie.Domain = "https://dimamykhnevych.github.io";
                //options.Cookie.SameSite = SameSiteMode.None;
                //options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                options.Cookie.HttpOnly = true;
                options.Cookie.Domain = null;
                options.Cookie.SameSite = SameSiteMode.None;
                //options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                builder.WithOrigins("http://localhost:4200",
                "https://dimamykhnevych.github.io")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                );
            });

            services.AddAuthorization();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment() || env.IsProduction())
            //{
                app.UseDeveloperExceptionPage();
            //}

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

            //var allowedOriginsCORS = Configuration.GetSection("AllowedOriginsCORS").Get<string[]>();
            //app.UseCors(builder =>
            //{
            //    builder.WithOrigins(allowedOriginsCORS)
            //        .AllowCredentials();
            //    builder.AllowAnyHeader();
            //    builder.AllowAnyMethod();
            //});

            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCookiePolicy();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Use((context, next) =>
            {
                context.Response.Headers.Add("test", "Set-Cookie");

                return next.Invoke();
            });
        }
    }
}
