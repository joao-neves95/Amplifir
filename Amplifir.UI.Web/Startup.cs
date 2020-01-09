using System;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using NSwag;
using NSwag.Generation.Processors.Security;
using Amplifir.ApplicationTypeFactory;
using Amplifir.Core.Interfaces;
using Amplifir.Core.Entities;
using Amplifir.Core.DomainServices;
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
            DotNetEnv.Env.Load();

            services.AddSwaggerDocument( options =>
            {
                options.PostProcess = doc =>
                {
                    doc.Info.Title = "Amplifir API";
                    doc.Info.Description = "The OpenAPI documentation of the Amplifir RestAPI.";
                    doc.Info.Contact = new NSwag.OpenApiContact
                    {
                        Name = "João Pedro Martins Neves (SHIVAYL)",
                        Url = "https://github.com/joao-neves95/"
                    };
                    doc.Info.License = new OpenApiLicense
                    {
                        Name = "Use under GNU Lesser General Public License v3.0",
                        Url = "https://github.com/joao-neves95/Amplifir/blob/master/LICENSE.md"
                    };
                };

                options.DocumentProcessors.Add( new SecurityDefinitionAppender( "JWT",
                    new OpenApiSecurityScheme()
                    {
                        Type = OpenApiSecuritySchemeType.ApiKey,
                        Name = "Authorization",
                        Description = "'Bearer ' + '[valid JWT token]'",
                        In = OpenApiSecurityApiKeyLocation.Header
                    } ) );

                options.OperationProcessors.Add( new OperationSecurityScopeProcessor( "JWT" ) );
            } );

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

            services.AddSingleton( typeof( IJWTService ), typeof( JWTService ) );
            services.AddSingleton( typeof( IBadWordsService ), typeof( BadWordsService ) );
            services.AddSingleton( typeof( IAppSecrets ), TypeFactory.Get( ApplicationTypes.AppSecrets ) );
            services.AddSingleton( typeof( IAppSettings ), TypeFactory.Get( ApplicationTypes.AppSettings ) );
            services.AddScoped( typeof( IAuditLogStore ), TypeFactory.Get( ApplicationTypes.AuditLogDapperStore ) );
            services.AddScoped( typeof( IAppUserStore<AppUser, int> ), TypeFactory.Get( ApplicationTypes.AppUserDapperStore ) );
            services.AddScoped( typeof( IPasswordService ), typeof( Argon2PasswordService ) );
            services.AddScoped( typeof( Amplifir.Core.Interfaces.IAuthenticationService ), typeof( Amplifir.Core.DomainServices.AuthenticationService ) );
            services.AddScoped( typeof( IAppUserProfileStore ), TypeFactory.Get( ApplicationTypes.AppUserProfileDapperStore ) );
            services.AddScoped( typeof( IUserProfileService ), typeof( UserProfileService ) );

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
            services.AddSpaStaticFiles( configuration => configuration.RootPath = "../Amplifir.UI.Presentation/dist" );
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

            // https://www.owasp.org/index.php/OWASP_Secure_Headers_Project#tab=Headers
            HeaderPolicyCollection headerPolicy = new HeaderPolicyCollection()
                .AddFrameOptionsDeny()
                .AddXssProtectionBlock()
                .AddContentTypeOptionsNoSniff()
                .AddReferrerPolicySameOrigin()
                // TODO: In production, increase max-age to 1 year.
                .AddStrictTransportSecurityMaxAgeIncludeSubDomains( 60 * 60 )
                .AddContentSecurityPolicy( builder =>
                {
                    builder.AddUpgradeInsecureRequests();
                    builder.AddBlockAllMixedContent();
                    builder.AddFrameSource().None();
                    builder.AddFrameAncestors().None();
                    builder.AddFormAction().OverHttps().Self();
                    builder.AddDefaultSrc().OverHttps().Self();
                    builder.AddObjectSrc().None();
                    // TODO: In the future add any CDN here.
                    builder.AddFontSrc().OverHttps().Self();
                    builder.AddImgSrc().OverHttps().Self();
                    builder.AddStyleSrc().OverHttps().Self();
                    builder.AddScriptSrc().OverHttps().Self();
                } )
                .AddCustomHeader( "X-Download-Options", "noopen" );

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

            app.UseOpenApi();
            app.UseSwaggerUi3( options => options.EnableTryItOut = false );

            app.UseRouting();
            app.UseAuthorization();

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

                spa.Options.SourcePath = "../Amplifir.UI.Presentation";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer( npmScript: "start" );
                }
            } );
        }
    }
}
