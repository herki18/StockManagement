using Microsoft.EntityFrameworkCore;
using StockManagement.Api.Contract.Entities;

namespace StockManagement.Api.DAL
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<BatchEntity> Batches { get; set; }
        public DbSet<FruitEntity> Fruits { get; set; }
        public DbSet<VarietyEntity> Varieties { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BatchEntity>().HasKey(x => x.Id);

            modelBuilder.Entity<FruitEntity>()
                .HasMany(c => c.Batches)
                .WithOne(e => e.Fruit)
                .IsRequired();

            modelBuilder.Entity<VarietyEntity>()
                .HasMany(c => c.Fruits)
                .WithOne(e => e.Variety)
                .IsRequired();

            // Seed Data
            SeedData.SeedInitialData(modelBuilder);
        }
    }
}