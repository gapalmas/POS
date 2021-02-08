using App.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Data.Config
{
    public class InventoryConfiguration : IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> entityTypeBuilder)
        {
            entityTypeBuilder.HasIndex(e => e.UnitId);

            entityTypeBuilder.HasIndex(e => e.WarehouseId);

            entityTypeBuilder.HasOne(d => d.Unit)
                .WithMany(p => p.Inventory)
                .HasForeignKey(d => d.UnitId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entityTypeBuilder.HasOne(d => d.Warehouse)
                .WithMany(p => p.Inventory)
                .HasForeignKey(d => d.WarehouseId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
