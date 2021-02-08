using App.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Data.Config
{
    public class PurchaseorderConfiguration : IEntityTypeConfiguration<Purchaseorder>
    {
        public void Configure(EntityTypeBuilder<Purchaseorder> entityTypeBuilder)
        {
            entityTypeBuilder.HasIndex(e => e.CustomerId);

            entityTypeBuilder.HasOne(d => d.Customer)
                .WithMany(p => p.Purchaseorder)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
