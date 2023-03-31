using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.eShopWeb.ApplicationCore.Entities;

namespace Microsoft.eShopWeb.Infrastructure.Data.Config;

public class CatalogLocalConfiguration : IEntityTypeConfiguration<CatalogLocal>
{
    public void Configure(EntityTypeBuilder<CatalogLocal> builder)
    {
        builder.HasKey(ci => ci.Id);

        builder.Property(ci => ci.Id)
           .UseHiLo("catalog_local_hilo")
           .IsRequired();

        builder.Property(cb => cb.Local)
            .IsRequired()
            .HasMaxLength(100);
    }
}
