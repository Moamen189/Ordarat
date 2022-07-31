using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordarat.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordarat.DataAccessLayer.Data.Config
{
    public class ProductConfigration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.PictureUrl).IsRequired();
            builder.Property(x => x.Price).HasColumnType("decimal(18,2)");
            //builder.HasOne(x =>x.ProductBrand).WithMany().HasForeignKey(p => p.ProductBrandId);
            //builder.HasOne(x => x.ProductType).WithMany().HasForeignKey(p => p.ProductTypeId);



        }
    }
}
