using App.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Data.Config
{
    public class TicketorderpayConfiguration : IEntityTypeConfiguration<Ticketorderpay>
    {
        public void Configure(EntityTypeBuilder<Ticketorderpay> entityTypeBuilder)
        {
            entityTypeBuilder.HasIndex(e => e.PurchaseOrderPayId);

            entityTypeBuilder.HasOne(d => d.PurchaseOrderPay)
                .WithMany(p => p.Ticketorderpay)
                .HasForeignKey(d => d.PurchaseOrderPayId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
