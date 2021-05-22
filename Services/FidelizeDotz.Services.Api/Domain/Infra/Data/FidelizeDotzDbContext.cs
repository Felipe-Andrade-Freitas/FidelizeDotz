using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FidelizeDotz.Services.Api.Domain.Entities;
using FidelizeDotz.Services.Api.Domain.Infra.Data.Mappings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FidelizeDotz.Services.Api.Domain.Infra.Data
{
    public class FidelizeDotzDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public FidelizeDotzDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<Dot> Dots { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }

        public CustomizeIdentityMapping CustomizeIdentityData => new CustomizeIdentityMapping();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder is null)
                return;

            base.OnModelCreating(modelBuilder);
            CustomizeIdentityData.OnModelCreatingMappings(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }

        public override int SaveChanges()
        {
            return SaveChanges(true);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            OnBeforeSaving();
            var num = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return num;
        }

        private void OnBeforeSaving()
        {
            var saveTime = DateTime.Now;
            ChangeTracker.Entries().Where(e =>
            {
                if (!e.Entity.GetType().IsSubclassOf(typeof(EntityBase)))
                    return false;
                return e.State == EntityState.Added || e.State == EntityState.Modified ||
                       e.State == EntityState.Deleted;
            }).ToList().ForEach(entry =>
            {
                entry.Property("CreatedAt").IsModified = false;
                entry.Property("UpdatedAt").IsModified = false;
                entry.Property("IsDeleted").IsModified = false;
                if (entry.State == EntityState.Added)
                {
                    entry.Property("CreatedAt").CurrentValue = saveTime;
                    entry.Property("CreatedAt").IsModified = true;
                }

                if (entry.State == EntityState.Deleted)
                {
                    entry.Property("IsDeleted").CurrentValue = true;
                    entry.Property("IsDeleted").IsModified = true;
                    entry.State = EntityState.Unchanged;
                }

                entry.Property("UpdatedAt").CurrentValue = saveTime;
                entry.Property("UpdatedAt").IsModified = true;
            });
        }
    }
}