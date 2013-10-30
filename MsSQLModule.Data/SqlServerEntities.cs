using MsSQLModule.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSQLModule.Data
{
    public class SqlServerEntities : DbContext
    {
        public SqlServerEntities()
            : base("SaleReportsDB")
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new Mappings.MeasureMapping());
            modelBuilder.Configurations.Add(new Mappings.VendorMapping());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Sale> Sales { get; set; }
        public DbSet<Supermarket> Supermarkets { get; set; }
        public DbSet<Measure> Measures { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
    }
}
