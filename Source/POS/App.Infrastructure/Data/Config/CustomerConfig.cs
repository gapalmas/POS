using App.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Data.Config
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> entityTypeBuilder)
        {
            entityTypeBuilder.Property(e => e.Cp).HasColumnType("decimal(5, 0)");
        }
    }
}
