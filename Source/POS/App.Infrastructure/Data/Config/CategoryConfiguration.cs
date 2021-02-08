using App.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Data.Config
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("category");

            entityTypeBuilder.Property(e => e.Id)
                .HasColumnName("id")
                .HasColumnType("int(11)");

            entityTypeBuilder.Property(e => e.Description)
                .IsRequired()
                .HasColumnName("description")
                .HasColumnType("varchar(24)")
                .HasCharSet("utf8")
                .HasCollation("utf8_spanish_ci");
        }
    }
}
