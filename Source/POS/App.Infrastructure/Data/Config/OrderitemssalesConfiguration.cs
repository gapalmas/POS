using App.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Data.Config
{
    public class OrderitemssalesConfiguration : IEntityTypeConfiguration<Orderitemssales>
    {
        public void Configure(EntityTypeBuilder<Orderitemssales> entityTypeBuilder)
        {
            entityTypeBuilder.HasIndex(e => e.ProductId);

            entityTypeBuilder.HasIndex(e => e.PurchaseOrderId);

            entityTypeBuilder.Property(e => e.Price).HasColumnType("decimal(18, 4)");

            entityTypeBuilder.HasOne(d => d.Product)
                .WithMany(p => p.Orderitemssales)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entityTypeBuilder.HasOne(d => d.PurchaseOrder)
                .WithMany(p => p.Orderitemssales)
                .HasForeignKey(d => d.PurchaseOrderId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
