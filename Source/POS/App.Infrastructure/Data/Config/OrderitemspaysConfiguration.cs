using App.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Data.Config
{
    public class OrderitemspaysConfiguration : IEntityTypeConfiguration<Orderitemspays>
    {
        public void Configure(EntityTypeBuilder<Orderitemspays> entityTypeBuilder)
        {
            entityTypeBuilder.HasIndex(e => e.ProductId);

            entityTypeBuilder.HasIndex(e => e.PurchaseOrderPayId);

            entityTypeBuilder.HasOne(d => d.Product)
                .WithMany(p => p.Orderitemspays)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entityTypeBuilder.HasOne(d => d.PurchaseOrderPay)
                .WithMany(p => p.Orderitemspays)
                .HasForeignKey(d => d.PurchaseOrderPayId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}