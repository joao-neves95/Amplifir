using System;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Amplifir.ApplicationTypeFactory;
using Amplifir.Core.Interfaces;
using Amplifir.Core.Entities;
using Amplifir.Core.Utilities;
using Microsoft.AspNetCore.HttpOverrides;
using Amplifir.Core.DomainServices;
using System.Text.Json;

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
            DotNetEnv.Env.Load();

            services.AddScoped( typeof( IDBContext ), _ => (IDBContext)Activator.CreateInstance(
                TypeFactory.Get( ApplicationTypes.DapperDBContext ),
                new object[] { StringUtils.BuildPostreSQLConnectionStringWithSSL(
                    DotNetEnv.Env.GetString( "DB_SERVER" ),
                    DotNetEnv.Env.GetString( "DB_PORT" ),
                    DotNetEnv.Env.GetString( "DB_DATABASE" ),
                    DotNetEnv.Env.GetString( "DB_USER" ),
                    DotNetEnv.Env.GetString( "DB_PASSWORD" )
                ) }
            ) );

            services.AddScoped( typeof( IAppUserStore<AppUser, int> ), TypeFactory.Get( ApplicationTypes.AppUserDapperStore ) );
            services.AddScoped( typeof( IPasswordService ), typeof( Argon2PasswordService ) );
            services.AddScoped( typeof( Amplifir.Core.Interfaces.IAuthenticationService ), typeof( Amplifir.Core.DomainServices.AuthenticationService ) );
            services.AddSingleton( typeof( IJWTService ), typeof( JWTService ) );

            services.AddControllersWithViews()
                    .AddJsonOptions( options => options.JsonSerializerOptions.PropertyNameCaseInsensitive = true );

            services.AddAuthentication( options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            } )
            .AddJwtBearer( options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ClockSkew = TimeSpan.FromMinutes( 5 ),
                    RequireSignedTokens = true,
                    RequireExpirationTime = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = DotNetEnv.Env.GetString( "JWT_ISSUER" ),
                    ValidAudience = DotNetEnv.Env.GetString( "JWT_ISSUER" ),
                    IssuerSigningKey = new SymmetricSecurityKey( Encoding.UTF8.GetBytes( DotNetEnv.Env.GetString( "JWT_KEY" ) ) )
                };
            } );

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
                app.UseHttpsRedirection();
            }

            app.UseForwardedHeaders( new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
                ForwardedHostHeaderName = "Anonymous",
                OriginalHostHeaderName = "Anonymous"
            } );

            app.UseAuthentication();
            app.UseStaticFiles();

            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseEndpoints( endpoints =>
            {
                 endpoints.MapControllerRoute(
                     name: "default",
                     pattern: "{controller}/{action=Index}/{id?}" );
            } );

            app.UseSpa( spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "../Amplifir.UI.Client";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer( npmScript: "start" );
                }
            } );
        }
    }
}
