using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using dotenv.net;
using Amplifir.ApplicationTypeFactory;
using Amplifir.Core.Interfaces;
using Amplifir.Core.Models;
using Amplifir.Core.Utilities;

namespace Amplifir.UI.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            DotEnv.Config();

            services.AddScoped( typeof( IDBContext ), _ => (IDBContext)Activator.CreateInstance(
                TypeFactory.Get( ApplicationTypes.DapperDBContext ),
                new object[] { StringUtils.BuildConnectionStringWithSSL(
                    Environment.GetEnvironmentVariable( "DB_SERVER" ),
                    Environment.GetEnvironmentVariable( "DB_PORT" ),
                    Environment.GetEnvironmentVariable( "DB_DATABASE" ),
                    Environment.GetEnvironmentVariable( "DB_USER" ),
                    Environment.GetEnvironmentVariable( "DB_PASSWORD" )
                ) }
            ) );

            services.AddScoped( typeof( IAppUserStore<AppUser, int> ), TypeFactory.Get( ApplicationTypes.AppUserDapperStore ) );
            services.AddScoped( typeof( IPasswordService ), TypeFactory.Get( ApplicationTypes.Argon2PasswordService ) );
            services.AddScoped( typeof( Core.Interfaces.IAuthenticationService ), TypeFactory.Get( ApplicationTypes.AuthenticationService ) );

            services.AddControllersWithViews();

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "../Amplifir.UI.Client/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler( "/Error" );
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}" );
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "../Amplifir.UI.Client";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer( npmScript: "start" );
                }
            });
        }
    }
}
