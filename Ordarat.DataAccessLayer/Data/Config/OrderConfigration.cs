using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordarat.DataAccessLayer.Entities.Order_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordarat.DataAccessLayer.Data.Config
{
    public class OrderConfigration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(O => O.ShipToAddress, Address => Address.WithOwner());
            builder.Property(O => O.Status).HasConversion
                (OStatus => OStatus.ToString(),
                OStatus =>  (OrderStatus)Enum.Parse(typeof(OrderStatus), OStatus)

                );

            builder.HasMany(O => O.Items).WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
