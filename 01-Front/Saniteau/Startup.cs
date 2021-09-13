using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));

            //database
            services.AddDistributedMemoryCache();

            services.AddHttpClient();
            services.AddScoped<AccessTokenManager>();
            services.AddScoped<PaymentService>();
            services.AddScoped<HttpMethodCaller>();

            services.AddDbContext<SaniteauDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
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
    }
}
