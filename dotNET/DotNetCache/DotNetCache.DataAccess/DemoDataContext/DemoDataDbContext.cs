using System.Data.Entity;
using System.Linq;
using DotNetCache.DataAccess.DemoDataEntities;

namespace DotNetCache.DataAccess.DemoDataContext
{
    public class DemoDataDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<LineItem> LineItems  { get; set; }
        public DbSet<Nation> Nations { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<PartSupp> PartSupps { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        public DemoDataDbContext() : base("")
        {
        }

        public DemoDataDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }



      /*  public int SaveAllChanges(bool invalidateCacheDependencies = true)
        {
            var changedEntityNames = GetChangedEntityNames();
            var result = base.SaveChanges();
            if (invalidateCacheDependencies)
            {
                new EFCacheServiceProvider().InvalidateCacheDependencies(changedEntityNames);
            }
            return result;
        }
        */
     /*   private string[] GetChangedEntityNames()
        {
            // Updated version of this method: \EFSecondLevelCache\EFSecondLevelCache.Tests\EFSecondLevelCache.TestDataLayer\DataLayer\SampleContext.cs
            return this.ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added ||
                            x.State == EntityState.Modified ||
                            x.State == EntityState.Deleted)
                .Select(x => System.Data.Entity.Core.Objects.ObjectContext.GetObjectType(x.Entity.GetType()).FullName)
                .Distinct()
                .ToArray();
        }*/

    }
}
