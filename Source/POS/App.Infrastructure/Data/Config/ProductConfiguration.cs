using App.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Data.Config
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> entityTypeBuilder)
        {
            entityTypeBuilder.HasIndex(e => e.CategoryId);

            entityTypeBuilder.HasIndex(e => e.ClasificationId);

            entityTypeBuilder.HasIndex(e => e.InventoryId);

            entityTypeBuilder.Property(e => e.Price).HasColumnType("decimal(18, 4)");

            entityTypeBuilder.HasOne(d => d.Category)
                .WithMany(p => p.Product)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entityTypeBuilder.HasOne(d => d.Clasification)
                .WithMany(p => p.Product)
                .HasForeignKey(d => d.ClasificationId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entityTypeBuilder.HasOne(d => d.Inventory)
                .WithMany(p => p.Product)
                .HasForeignKey(d => d.InventoryId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
