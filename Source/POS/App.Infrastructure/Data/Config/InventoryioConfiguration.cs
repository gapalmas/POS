using App.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Data.Config
{
    public class InventoryioConfiguration : IEntityTypeConfiguration<Inventoryio>
    {
        public void Configure(EntityTypeBuilder<Inventoryio> entityTypeBuilder)
        {
            entityTypeBuilder.HasIndex(e => e.InventoryId);

            entityTypeBuilder.Property(e => e.Price).HasColumnType("decimal(18, 4)");

            entityTypeBuilder.HasOne(d => d.Inventory)
                .WithMany(p => p.Inventoryio)
                .HasForeignKey(d => d.InventoryId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
