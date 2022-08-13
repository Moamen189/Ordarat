using Microsoft.EntityFrameworkCore;
using Ordarat.DataAccessLayer.Data.Config;
using Ordarat.DataAccessLayer.Entities;
using Ordarat.DataAccessLayer.Entities.Order_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ordarat.DataAccessLayer
{
    public class StroreContext : DbContext
    {
        public StroreContext(DbContextOptions<StroreContext> options):base(options)
        {

        }

        public DbSet<Product> Product { get; set; }
        public DbSet<ProductType> ProductType { get; set; }

        public DbSet<ProductBrand> ProductBrand { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<DelivaryMethod> DeliveryMethods { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.ApplyConfiguration(new ProductConfigration());

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
