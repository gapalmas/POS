using App.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Data.Config
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> entityTypeBuilder)
        {
            entityTypeBuilder.Property(e => e.Cp).HasColumnType("decimal(5, 0)");
        }
    }
}
