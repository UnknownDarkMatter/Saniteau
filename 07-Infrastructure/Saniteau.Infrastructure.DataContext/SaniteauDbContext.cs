using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Saniteau.Infrastructure.DataModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Saniteau.Infrastructure.DataModel.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Saniteau.Infrastructure.DataContext
{
    public class SaniteauDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int> 
    {
        public DbSet<AbonnéModel> Abonnés { get; set; }
        public DbSet<AdresseModel> Adresses { get; set; }
        public DbSet<AdressePDLModel> AdressesPDL { get; set; }
        public DbSet<AppairageCompteurModel> AppairagesCompteurs { get; set; }
        public DbSet<CompteurModel> Compteurs { get; set; }
        public DbSet<IndexCompteurModel> IndexesCompteur { get; set; }
        public DbSet<PDLModel> PDL { get; set; }
        public DbSet<PompeModel> Pompes { get; set; }
        public DbSet<FacturationModel> Facturations { get; set; }
        public DbSet<FacturationLigneModel> FacturationLignes { get; set; }
        public DbSet<DelegantModel> Delegants { get; set; }
        public DbSet<PayeDelegantModel> PayesDelegant { get; set; }
        public DbSet<PayeDelegantLigneModel> PayesDelegantLignes { get; set; }
        public DbSet<IndexPayéParDelegantModel> IndexesPayésParDelegant { get; set; }
        public DbSet<FacturePayeeAuDelegantModel> FacturesPayeesAuDelegant { get; set; }

        public SaniteauDbContext() : base()
        {

        }

        public SaniteauDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RefreshToken>()
                .HasOne(p => p.User)
                .WithMany(p => p.RefreshTokens);

            modelBuilder.Entity<ApplicationUser>(b => { b.ToTable("Users"); });
            modelBuilder.Entity<IdentityUserClaim<int>>(b => { b.ToTable("UserClaims"); });
            modelBuilder.Entity<IdentityUserLogin<int>>(b => { b.ToTable("UserLogins"); });
            modelBuilder.Entity<IdentityUserToken<int>>(b => { b.ToTable("UserTokens"); });
            modelBuilder.Entity<ApplicationRole>(b => { b.ToTable("Roles"); });
            modelBuilder.Entity<IdentityRoleClaim<int>>(b => { b.ToTable("RoleClaims"); });
            modelBuilder.Entity<IdentityUserRole<int>>(b => { b.ToTable("UserRoles"); });

        }
    }
}
