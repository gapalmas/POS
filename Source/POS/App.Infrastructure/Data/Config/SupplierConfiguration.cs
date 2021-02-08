using App.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Data.Config
{
    public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> entityTypeBuilder)
        {
            entityTypeBuilder.Property(e => e.Cp).HasColumnType("decimal(5, 0)");
        }
    }
}
