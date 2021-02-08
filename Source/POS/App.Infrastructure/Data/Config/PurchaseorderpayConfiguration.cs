using App.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Data.Config
{
    public class PurchaseorderpayConfiguration : IEntityTypeConfiguration<Purchaseorderpay>
    {
        public void Configure(EntityTypeBuilder<Purchaseorderpay> entityTypeBuilder)
        {
            entityTypeBuilder.HasIndex(e => e.SupplierId);

            entityTypeBuilder.HasOne(d => d.Supplier)
                .WithMany(p => p.Purchaseorderpay)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
