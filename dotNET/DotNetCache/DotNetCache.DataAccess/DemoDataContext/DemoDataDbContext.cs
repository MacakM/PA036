using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public DemoDataDbContext()
        {
        }

        public DemoDataDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }
    }
}
