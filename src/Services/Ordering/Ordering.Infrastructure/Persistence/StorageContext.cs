using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Common;
using Ordering.Domain.Entities;

namespace Ordering.Infrastructure.Persistence
{
    public class StorageContext : DbContext
    {
        public StorageContext(DbContextOptions<StorageContext> opts)
            : base(opts)
        {
            
        }

        public DbSet<Order> Orders { get; set; }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            UpdateTrackableData();

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            UpdateTrackableData();

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        // TODO: need to change logic of setting '*By' properties
        private void UpdateTrackableData()
        {
            var addedEntities = ChangeTracker.Entries().Where(e => e.State == EntityState.Added).ToList();
            addedEntities.ForEach(e =>
            {
                if (e.Entity is ITrackable)
                {
                    e.Property("CreatedAt").CurrentValue = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                    e.Property("CreatedBy").CurrentValue = "Admin";
                }
            });

            var editedEntities = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified).ToList();
            editedEntities.ForEach(e =>
            {
                if (e.Entity is ITrackable)
                {
                    e.Property("UpdatedAt").CurrentValue = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                    e.Property("UpdatedBy").CurrentValue = "Admin";
                }
            });
        }



        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.use
        //}
    }
}
 