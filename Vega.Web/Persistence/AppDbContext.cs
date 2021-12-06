using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using Vega.Web.Models;
using Pomelo.EntityFrameworkCore.MySql;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace Vega.Web.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions)
        {
            this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            this.ChangeTracker.LazyLoadingEnabled = false;
            this.ChangeTracker.AutoDetectChangesEnabled = false;          
        }

        public DbSet<Model> Models { get; set; }
        public DbSet<Make> Makes { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleFeature> VehicleFeatures { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {            
            //IConfigurationRoot configuration = new ConfigurationBuilder()
            //                                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            //                                    .AddJsonFile("appsettings.json")
            //                                    .Build();
            
            //optionsBuilder.UseMySql(configuration.GetConnectionString("DefaultConnection"), mySqlOptions => mySqlOptions.ServerVersion(new Version(10, 5, 0), ServerType.MariaDb));
            //optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Make>().Property(m => m.Name).HasMaxLength(500).IsRequired();
            modelBuilder.Entity<Model>().Property(m => m.Name).HasMaxLength(500).IsRequired();
            modelBuilder.Entity<Model>().Property(m => m.MakeId).IsRequired();
            
            modelBuilder.Entity<Feature>().Property(p => p.Name).HasMaxLength(255).IsRequired();
            
            modelBuilder.Entity<Vehicle>().Property(p => p.IsRegistered).IsRequired();
            modelBuilder.Entity<Vehicle>().Property(p => p.LastUpdate).IsRequired();
            modelBuilder.Entity<Vehicle>().Property(p => p.PersonName).HasMaxLength(255).IsRequired();
            modelBuilder.Entity<Vehicle>().Property(p => p.PersonPhone).HasMaxLength(255);
            modelBuilder.Entity<Vehicle>().Property(p => p.PersonEmail).HasMaxLength(255);

            modelBuilder.Entity<VehicleFeature>().HasKey(k => new { k.VehicleId, k.FeatureId });
        }
    }
}