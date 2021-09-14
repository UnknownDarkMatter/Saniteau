using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbUp;
using DbUp.Engine;
using log4net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Saniteau.Auth;
using Saniteau.Database;
using Saniteau.Facturation.Payment.Services;
using Saniteau.Infrastructure.DataContext;
using Saniteau.Infrastructure.DataModel.Identity;
using Saniteau.Models;

namespace Saniteau
{
    public class Startup
    {
        private const string SecretKey = "iNivDmHLpUA223sqsfhqGbMRdRj1PVkH"; // todo: get this from somewhere secure
        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

        private static ILog Log = LogManager.GetLogger(typeof(Startup));

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            UpgradeDatabase(connectionString);

            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));

            //database
            services.AddDistributedMemoryCache();

            services.AddHttpClient();
            services.AddScoped<AccessTokenManager>();
            services.AddScoped<PaymentService>();
            services.AddScoped<HttpMethodCaller>();

            services.AddDbContext<SaniteauDbContext>(opt => opt.UseSqlServer(connectionString));
            services.AddScoped<SaniteauDbContext>();
            services.AddScoped<IDbContextFactory, SaniteauDbContextFactory>();

            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.User.RequireUniqueEmail = false;
                options.Password.RequiredLength = 1;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
            })
            .AddEntityFrameworkStores<SaniteauDbContext>()
            .AddDefaultTokenProviders();


            services.AddSingleton<IJwtFactory, JwtFactory>();

            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options => { 
                    options.RequireHttpsMetadata = false; 
                    options.SaveToken = true; 
                    options.TokenValidationParameters = new TokenValidationParameters { 
                        ValidateIssuer = false, 
                        ValidateAudience = false, 
                        ValidateLifetime = true, 
                        ValidateIssuerSigningKey = true, 
                        ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)], 
                        ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)], 
                        IssuerSigningKey = _signingKey,
                        ClockSkew = TimeSpan.FromMinutes(Constants.JWT.TokenClockStewMinutes)
                    }; 
                });




            services.AddAuthorization(options =>
            {
                options.AddPolicy(Constants.JWT.ApiAccessPolicyName, policy => policy.RequireClaim(Constants.JWT.JwtClaimIdentifiers.Rol, Constants.JWT.JwtClaims.ApiAccess));
                //pour debugger : on voit que si le context.User ClaimsPrincipal n'est pas authentifié on a aucun claims
                //options.AddPolicy(Constants.Strings.ApiAccessPolicyName, policy =>
                //{
                //    policy.RequireAssertion(context => {
                //        if(context.User.HasClaim(Constants.Strings.JwtClaimIdentifiers.Rol, Constants.Strings.JwtClaims.ApiAccess))
                //        {
                //            return true;
                //        }
                //        return false;
                //    });
                //});
            });

            //Compteurs.Repository
            services.AddScoped<Compteurs.Repository.RéférentielAbonnésOnEfCore>();
            services.AddScoped<Compteurs.Repository.RéférentielAppairageOnEfCore>();
            services.AddScoped<Compteurs.Repository.RéférentielCompteursOnEfCore>();
            services.AddScoped<Compteurs.Repository.RéférentielIndexesCompteursOnEfCore>();
            services.AddScoped<Compteurs.Repository.RéférentielPDLOnEfCore>();

            //Relevés Repositories
            services.AddScoped<Releves.Repository.RéférentielCompteursOnEfCore>();
            services.AddScoped<Releves.Repository.RéférentielIndexesCompteursOnEfCore>();
            services.AddScoped<Releves.Repository.RéférentielPompesOnEfCore>();

            //Facturation Repositories
            services.AddScoped<Facturation.Repository.RéférentielAbonnésOnEfCore>();
            services.AddScoped<Facturation.Repository.RéférentielAppairageOnEfCore>();
            services.AddScoped<Facturation.Repository.RéférentielCompteursOnEfCore>();
            services.AddScoped<Facturation.Repository.RéférentielFacturationOnEfCore>();
            services.AddScoped<Facturation.Repository.RéférentielIndexesCompteursOnEfCore>();
            services.AddScoped<Facturation.Repository.RéférentielPDLOnEfCore>();

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Dashboard}/{action=Index}/{id?}");
            });
        }

        private void UpgradeDatabase(string connectionString)
        {
            var engine = DeployChanges.To
            .SqlDatabase(connectionString)
            .WithScriptsEmbeddedInAssembly(typeof(SaniteauDbContext).Assembly)
            .LogToAutodetectedLog()
            .Build();

            Log.Debug("DISCOVERED SCRIPTS :");
            foreach (SqlScript discoveredScript in engine.GetDiscoveredScripts())
            {
                Log.Debug(discoveredScript.Name);
            }
            Log.Debug("SCRIPTS TO EXECUTE :");
            foreach (SqlScript scriptToExecute in engine.GetScriptsToExecute())
            {
                Log.Debug(scriptToExecute.Name);
            }

            bool isUpgradeRequired = engine.IsUpgradeRequired();
            if (!isUpgradeRequired)
            {
                Log.Info("Database CaNavire is already up to date.");
                return;
            }

            Log.Info("Database CaNavire need an upgrade.");

            DatabaseUpgradeResult migrationResult = engine.PerformUpgrade();

            if (!migrationResult.Successful)
            {
                Log.Fatal("Database CaNavire upgrade failed.", migrationResult.Error);
                throw new Exception("Database CaNavire upgrade failed.", migrationResult.Error);
            }

            Log.Info("Database CaNavire is up to date.");
        }
    }
}
