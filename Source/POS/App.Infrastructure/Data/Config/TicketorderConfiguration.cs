using App.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Data.Config
{
    public class TicketorderConfiguration : IEntityTypeConfiguration<Ticketorder>
    {
        public void Configure(EntityTypeBuilder<Ticketorder> entityTypeBuilder)
        {
            entityTypeBuilder.HasIndex(e => e.PurchaseOrderId);

            entityTypeBuilder.HasOne(d => d.PurchaseOrder)
                .WithMany(p => p.Ticketorder)
                .HasForeignKey(d => d.PurchaseOrderId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
