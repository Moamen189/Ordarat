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
    internal class DeliveryMethodConfigration : IEntityTypeConfiguration<DelivaryMethod>
    {
        public void Configure(EntityTypeBuilder<DelivaryMethod> builder)
        {
            builder.Property(DM => DM.Cost)
                .HasColumnType("decimal(18,2)");
        }
    }
}
